
using CalculationOfEmployeeSalaries.Core.DomainModels;

namespace CalculationOfEmployeeSalaries.Core
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByNationalCode(string nationalcode);
        Task<Employee?> TryGetByNationalCode(string nationalcode);
        Task<Employee> Get(string nationalCode, DateTime date);
        Task<Employee> GetRange(string nationalCode, DateTime fromDate, DateTime toDate);
        Task<Employee> Add(Employee employee);
    }
}
