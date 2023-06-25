using CalculationOfEmployeeSalaries.Infrastructure.DataAccess;
using System.Data;
using Xunit;

namespace CalculationOfEmployeeSalaries.UnitTests
{
    public class EmployeeRepositoryTest : IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;
        private readonly IDbConnection _dbConnection;
        public EmployeeRepositoryTest(DbContextFixture fixture)
        {
            _fixture = fixture;
        }
        public void AddEmploeeSalary_test()
        {

            var repository = new EmployeeRepository(_fixture.DbContextFake,_fixture.DbConnectionFake);


        }
    }
}
