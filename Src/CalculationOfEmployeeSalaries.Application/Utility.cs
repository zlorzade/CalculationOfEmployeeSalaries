using CalculationOfEmployeeSalaries.Application.ApiModels;
using CalculationOfEmployeeSalaries.Core.DomainModels;
using OvetimePolicies;
using System.Globalization;

namespace CalculationOfEmployeeSalaries.Application
{
    public static class Utility
    {
        public static string ConvertToPersian(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}", pc.GetYear(date), pc.GetMonth(date),  pc.GetDayOfMonth(date));
        }

        public static DateTime ConvertToDateTime(this string str)
        {
            string[] dateFormats = new[] { "yyyyMMdd" };
            CultureInfo provider = new("fa-IR");
            return DateTime.ParseExact(str, dateFormats, provider,
            DateTimeStyles.AdjustToUniversal);
        }

        public static OutputEmployeeDto CreateOutputEmployeeDto(Employee employee)
        {
            EmployeeSalary ps = employee.EmployeeSalaries.Last();
            OutputEmployeeDto output = new()
            {
                NationalCode = employee.NationalCode,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BasicSalary = ps.BasicSalary,
                Transportation = ps.Transportation,
                Date = ps.Date.ConvertToPersian(),
                Salary = ps.Salary,
                Allowance = ps.Allowance,
                OverTime = ps.OverTime,
                Calculator = ps.Calculator.ToString()
            };
            return output;
        }

        public static ICalculator GetCalculatorMethod(Calculator calculator)
        {
            ICalculator calculatorMNethod = calculator switch
            {
                Calculator.CalculatorA => new CalculatorA(),
                Calculator.CalculatorB => new CalculatorB(),
                Calculator.CalculatorC => new CalculatorC(),
                _ => new CalculatorA(),
            };
            return calculatorMNethod;

        }
    }
}
