﻿namespace CalculationOfEmployeeSalaries.Application.ApiModels
{
    public class SalaryDto
    {
        public string NationalCode { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Transportation { get; set; }
        public string Date { get; set; }
    }
}