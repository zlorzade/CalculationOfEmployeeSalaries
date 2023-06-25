namespace CalculationOfEmployeeSalaries.Application.ApiModels
{
    public class OutputSalaryDto
    {
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Transportation { get; set; }
        public decimal Salary { get; set; }
        public decimal OverTime { get; set; }
        public string Date { get; set; }
        public string Calculator { get; set; }  
    }
}
