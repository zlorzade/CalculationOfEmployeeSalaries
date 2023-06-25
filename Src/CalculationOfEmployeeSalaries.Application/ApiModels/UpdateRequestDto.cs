using CalculationOfEmployeeSalaries.Core.DomainModels;

namespace CalculationOfEmployeeSalaries.Application.ApiModels
{
    public class UpdateRequestDto
    {
        public SalaryDto Data { get; set; }

        public Calculator OverTimeCalculator { get; set; }

    }
}

