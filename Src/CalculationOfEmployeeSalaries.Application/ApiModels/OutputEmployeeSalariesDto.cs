namespace CalculationOfEmployeeSalaries.Application.ApiModels
{
    public class OutputEmployeeSalariesDto
    {
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<OutputSalaryDto> Salaries { get; init; }=new List<OutputSalaryDto>();   
    }
}
