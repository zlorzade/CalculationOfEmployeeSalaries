using Dapper;
using CalculationOfEmployeeSalaries.Core;
using CalculationOfEmployeeSalaries.Core.DomainModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CalculationOfEmployeeSalaries.Infrastructure.DataAccess
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _context;
        private readonly IDbConnection _connection;

        public EmployeeRepository(ApplicationContext context, IDbConnection connection)
        {
            _context = context;
            _connection = connection;
        }

        public Task<Employee?>  TryGetByNationalCode(string nationalcode)
        {
            return _context.Employees.Include(s => s.EmployeeSalaries).SingleOrDefaultAsync(c => c.NationalCode == nationalcode);
        }
        public async Task<Employee> GetByNationalCode(string nationalcode)
        {
            var employee = await _context.Employees.Include(s => s.EmployeeSalaries).SingleOrDefaultAsync(c => c.NationalCode == nationalcode);
            if (employee == null)
                throw new ArgumentException("Employee not found");
            return employee;
        }

        public async Task<Employee> Get(string nationalCode, DateTime date)
        {
            var parameters = new { NationalCode = nationalCode, Year = date.Year, Month = date.Month };
            var sql = @"SELECT e.Id ,e.NationalCode, e.FirstName, e.LastName, s.EmployeeId, s.BasicSalary, s.Allowance ,s.Transportation ,s.Salary ,s.Date,s.OverTime, s.Calculator
                FROM Employees e 
                INNER JOIN Salaries s ON e.Id = s.EmployeeId
                WHERE e.Nationalcode = @NationalCode AND Year(s.Date) = @Year and Month(s.Date) = @Month AND s.IsDeleted = 0";

            var result = await _connection.QueryAsync<Employee, EmployeeSalary, Employee>
                     (sql, (employee, salary) =>
                     {
                         employee.EmployeeSalaries.Add(salary); return employee;
                     }, parameters, splitOn: "EmployeeId");

            if (result is null)
                throw new Exception("Not found.");

            return result.SingleOrDefault();
        }

        public async Task<Employee> GetRange(string nationalCode, DateTime fromDate, DateTime toDate)
        {
            var parameters = new { NationalCode = nationalCode, FromDate = fromDate, ToDate = toDate };
            string sql =
                    "SELECT * FROM Employees e WHERE e.NationalCode = @NationalCode;" +
                    "SELECT * FROM Salaries s WHERE s.EmployeeId = (SELECT Id FROM Employees WHERE NationalCode = @NationalCode) and s.Date BETWEEN  @FromDate AND @ToDate AND s.IsDeleted = 0";

            using var results =await  _connection.QueryMultipleAsync(sql, parameters);

            var employee = results.Read<Employee>().SingleOrDefault();
            if (employee == null)
                throw new Exception("Employee not found");

            var salaries = results.Read<EmployeeSalary>().ToList();

            if (employee is not null && salaries is not null)
            {
                salaries.ForEach(x => employee.EmployeeSalaries.Add(x));
            }
            return employee;
        }
        public async Task<Employee> Add(Employee employee)
        {
            return (await _context.Set<Employee>().AddAsync(employee)).Entity;

        }



    }
}
