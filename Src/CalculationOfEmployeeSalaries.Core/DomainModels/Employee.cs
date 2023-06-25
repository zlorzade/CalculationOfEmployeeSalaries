using OvetimePolicies;

namespace CalculationOfEmployeeSalaries.Core.DomainModels
{
    public class Employee
    {
        private Employee()
        {

        }
        public int Id { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }



        public List<EmployeeSalary> EmployeeSalaries { get; init; } = new List<EmployeeSalary>();

        private void CheckDuplicateSalary()
        {
            if (EmployeeSalaries.GroupBy(x => new { x.Date.Year, x.Date.Month }).Any(g => g.Count() > 1))
            {
                throw new Exception("Employee salary is duplicate");
            }
        }

        public void AddEmployeeSalary(decimal basicSalary, decimal allowance, decimal transportation, DateTime date, ICalculator calculator)
        {
            var overTime = calculator.Calculat(allowance, basicSalary);
            EmployeeSalaries.Add(new EmployeeSalary()
            {
                Allowance = allowance,
                BasicSalary = basicSalary,
                Transportation = transportation,
                OverTime = overTime,
                Salary = CalculateSalary(basicSalary, allowance, transportation, overTime),
                Date = date,
                Calculator = Enum.Parse<Calculator>(calculator.ToString())
            });
            CheckDuplicateSalary();
        }

        public static Employee Create(string nationalCode, string firstName, string lastName)
        {

            var employee = new Employee()
            {
                NationalCode = nationalCode,
                FirstName = firstName,
                LastName = lastName,
            };

            return employee;


        }

        public void UpdateEmployeeSalary(decimal basicSalary, decimal allowance, decimal transportation, DateTime date, ICalculator calculator)
        {


            var emploeeSalary = EmployeeSalaries.SingleOrDefault(c => c.Date.Year == date.Year && c.Date.Month == date.Month);
            if (emploeeSalary == null)
                throw new Exception("Employee salary not found.");
            var overTime = calculator.Calculat(allowance, basicSalary);
            emploeeSalary.Allowance = allowance;
            emploeeSalary.Transportation = transportation;
            emploeeSalary.BasicSalary = basicSalary;
            emploeeSalary.Salary = CalculateSalary(basicSalary, allowance, transportation, overTime);
            emploeeSalary.OverTime = calculator.Calculat(allowance, basicSalary);
            emploeeSalary.Date = date;
            emploeeSalary.Calculator = Enum.Parse<Calculator>(calculator.ToString());

        }
        public void DeleteEmployeeSalary(DateTime date)
        {
            var employeeSalary = EmployeeSalaries.SingleOrDefault(c => c.Date.Year == date.Year && c.Date.Month == date.Month);
            if (employeeSalary == null)
                throw new Exception("Employee salary not found.");
            employeeSalary.IsDeleted = true;
        }

        private decimal CalculateSalary(decimal basicSalary, decimal allowance, decimal transportation, decimal overTime)
        {
            return basicSalary + allowance + transportation + overTime - CalculateTax(basicSalary, allowance);
        }

        private decimal CalculateTax(decimal basicSalary, decimal allowance)
        {
            return (basicSalary + allowance) * 10 / 100;
        }


    }
    public enum Calculator
    {
        CalculatorA = 1,
        CalculatorB,
        CalculatorC

    }


}