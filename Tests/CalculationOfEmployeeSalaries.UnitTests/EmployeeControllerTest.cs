
using CalculationOfEmployeeSalaries.Application;
using CalculationOfEmployeeSalaries.Application.ApiModels;
using CalculationOfEmployeeSalaries.Application.Controllers;
using CalculationOfEmployeeSalaries.Core;
using CalculationOfEmployeeSalaries.Core.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OvetimePolicies;
using Xunit;
using Calculator = CalculationOfEmployeeSalaries.Core.DomainModels.Calculator;

namespace CalculationOfEmployeeSalaries.UnitTests
{
    public class EmployeeControllerTest
    {

        [Fact]
        public  async void AddEmployeeSalary_save()
        {
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

            var repositoryMock = new Mock<IEmployeeRepository>();
            repositoryMock.Setup(m => m.Add(It.IsAny<Employee>())).ReturnsAsync(employee);

            var unitOfworkMock = new Mock<IUnitOfWork>();
            var _controller = new EmployeeController(repositoryMock.Object, unitOfworkMock.Object);


            var output = new OutputEmployeeDto()
            {
                NationalCode = "1234567890",
                FirstName = "zahra",
                LastName = "ahmadi",
                BasicSalary = 11000000,
                Transportation = 900000,
                Allowance = 8000000,
                Date = "1401/2/2",
                Salary = 37000000,
                Calculator = Calculator.CalculatorB.ToString(),
                OverTime = 19000000

            };

            var controllerResult = await _controller.Add(requestDto);
            var result = (controllerResult as ObjectResult)?.Value as OutputEmployeeDto;

            Assert.NotNull(result);
            Assert.Equal(output.FirstName, result.FirstName);
            Assert.Equal(output.LastName, result.LastName);
            Assert.Equal(output.Salary, result.Salary);
            Assert.Equal(output.BasicSalary, result.BasicSalary);
            Assert.Equal(output.OverTime, result.OverTime);
            Assert.Equal(output.Transportation, result.Transportation);
            Assert.Equal(output.Allowance, result.Allowance);
            Assert.Equal(output.Date, result.Date);
            Assert.Equal(output.Calculator, result.Calculator);
            Assert.Equal(output.NationalCode, result.NationalCode);
        }


    }
}