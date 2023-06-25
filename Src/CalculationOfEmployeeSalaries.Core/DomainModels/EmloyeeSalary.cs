
namespace CalculationOfEmployeeSalaries.Core.DomainModels
{
    public class EmployeeSalary
    {

        public int Id { get;  set; }
        public int EmployeeId { get;  set; }
        public decimal BasicSalary { get;  set; }
        public decimal Allowance { get;  set; }
        public decimal Transportation { get;  set; }
        public decimal Salary { get;  set; }
        public decimal OverTime { get;  set; }
        public DateTime Date { get;  set; }
        public bool IsDeleted { get;  set; }
        public Calculator Calculator { get; set; }





    }
}
