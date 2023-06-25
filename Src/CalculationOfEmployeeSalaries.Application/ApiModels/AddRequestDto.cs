using CalculationOfEmployeeSalaries.Core.DomainModels;

namespace CalculationOfEmployeeSalaries.Application.ApiModels
{
    public class AddRequestDto
    {
        public InputEmployeeDto Data { get; set; }
        public Calculator OverTimeCalculator { get; set; }
    }
}

