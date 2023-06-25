using CalculationOfEmployeeSalaries.Application;
using CalculationOfEmployeeSalaries.Application.ApiModels;
using CalculationOfEmployeeSalaries.Core;
using CalculationOfEmployeeSalaries.Core.DomainModels;
using Microsoft.AspNetCore.Mvc;
using OvetimePolicies;

namespace CalculationOfEmployeeSalaries.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IEmployeeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateRequestDto request)
        {
            if (request is null)
                return BadRequest(ModelState);
            var employee = await _repository.GetByNationalCode(request.Data.NationalCode);
            ICalculator calculator = Utility.GetCalculatorMethod(request.OverTimeCalculator);

            employee.UpdateEmployeeSalary(request.Data.BasicSalary, request.Data.Allowance, request.Data.Transportation, request.Data.Date.ConvertToDateTime(), calculator);

            _ = _unitOfWork.SaveAsync();
            var output = Utility.CreateOutputEmployeeDto(employee);
            return Ok(output);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddRequestDto request)
        {
            if (request is null)
                return BadRequest(ModelState);

            var employee = await _repository.TryGetByNationalCode(request.Data.NationalCode);
            if (employee == null)
                employee = Employee.Create(request.Data.NationalCode, request.Data.FirstName, request.Data.LastName);

            ICalculator calculator = Utility.GetCalculatorMethod(request.OverTimeCalculator);

            employee.AddEmployeeSalary(request.Data.BasicSalary, request.Data.Allowance, request.Data.Transportation, request.Data.Date.ConvertToDateTime(), calculator);

            if (employee.Id == 0)
                _ = _repository.Add(employee);
            _ = _unitOfWork.SaveAsync();

            var output = Utility.CreateOutputEmployeeDto(employee);
            return Ok(output);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteRequestDto request)
        {
            var employee =await _repository.GetByNationalCode(request.NationalCode);

            employee.DeleteEmployeeSalary(request.Date.ConvertToDateTime());
            _ = _unitOfWork.SaveAsync();
            return Ok("Successfully removed.");
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] GetRequestDto request)
        {
            var employeeSalary =await  _repository.Get(request.NationalCode, request.Date.ConvertToDateTime());
            var output = Utility.CreateOutputEmployeeDto(employeeSalary);
            return Ok(output);
        }

        [HttpGet("GetRange")]
        public async Task<IActionResult> GetRange([FromQuery] GetRangeRequest request)
        {
            var fromDate = new DateTime(request.FromDate.ConvertToDateTime().Year, request.FromDate.ConvertToDateTime().Month, 1);
            var toDate = new DateTime(request.ToDate.ConvertToDateTime().Year, request.ToDate.ConvertToDateTime().Month + 1, 1);
            var employee =await _repository.GetRange(request.NationalCode, fromDate, toDate);
            OutputEmployeeSalariesDto output = new()
            {
                NationalCode = employee.NationalCode,
                FirstName = employee.FirstName,
                LastName = employee.LastName,

            };
            employee.EmployeeSalaries.ForEach(p => output.Salaries.Add(new OutputSalaryDto()
            {
                Allowance = p.Allowance,
                BasicSalary = p.BasicSalary,
                Transportation = p.Transportation,
                Date = p.Date.ConvertToPersian(),
                Salary = p.Salary,
                OverTime = p.OverTime,
                Calculator = p.Calculator.ToString()
            }));

            return Ok(output);
        }


    }
}

