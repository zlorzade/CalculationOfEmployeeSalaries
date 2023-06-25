using CalculationOfEmployeeSalaries.Application;
using CalculationOfEmployeeSalaries.Application.ApiModels;
using CalculationOfEmployeeSalaries.Core.DomainModels;
using CalculationOfEmployeeSalaries.Infrastructure.DataAccess;
using Moq;
using OvetimePolicies;
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
        [Fact]
        public async void AddEmploeeSalary_test()
        {

            var repository = new EmployeeRepository(_fixture.DbContextFake, _fixture.DbConnectionFake);
            var requestDto = new AddRequestDto()
            {
                OverTimeCalculator = Calculator.CalculatorB,

                Data = new InputEmployeeDto()
                {
                    FirstName = "zahra",
                    LastName = "ahmadi",
                    NationalCode = "1234567890",
                    BasicSalary = 11000000,
                    Transportation = 900000,
                    Allowance = 8000000,
                    Date = "14010202"
                }
            };
            var calculatorMock = new Mock<ICalculator>();
            calculatorMock.Setup(c => c.ToString()).Returns(requestDto.OverTimeCalculator.ToString());

            var employee = Employee.Create(requestDto.Data.NationalCode, requestDto.Data.FirstName, requestDto.Data.LastName);
            employee.AddEmployeeSalary(requestDto.Data.BasicSalary, requestDto.Data.Allowance, requestDto.Data.Transportation,
                requestDto.Data.Date.ConvertToDateTime(), calculatorMock.Object);

            var result = await repository.Add(employee);

            Assert.NotNull(result);
            Assert.Equal(employee, result);

        }
    }
}
