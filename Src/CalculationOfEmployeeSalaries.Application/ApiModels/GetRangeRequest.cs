namespace CalculationOfEmployeeSalaries.Application.ApiModels
{
    public class GetRangeRequest
    {
        public string NationalCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }
}

