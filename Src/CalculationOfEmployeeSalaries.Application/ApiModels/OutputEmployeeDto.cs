namespace CalculationOfEmployeeSalaries.Application.ApiModels
{
    public class OutputEmployeeDto
    {
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Transportation { get; set; }
        public decimal Salary { get; set; }
        public decimal OverTime { get; set; }
        public string Date { get; set; }
        public string Calculator { get; set; }
    }
}
