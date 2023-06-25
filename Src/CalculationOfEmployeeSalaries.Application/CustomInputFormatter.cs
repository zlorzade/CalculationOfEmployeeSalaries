using CalculationOfEmployeeSalaries.Application.ApiModels;
using CalculationOfEmployeeSalaries.Core.DomainModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CalculationOfEmployeeSalaries.Application;

public class CustomInputFormatter : TextInputFormatter
{
    public CustomInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/custom"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }
    const string Line1 = "Line1:";
    const string Line2 = "Line2:";

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (encoding == null)
        {
            throw new ArgumentNullException(nameof(encoding));
        }

        var request = context.HttpContext.Request;

        using var reader = new StreamReader(request.Body, encoding);
        try
        {
            var content = await reader.ReadToEndAsync();
            content = content.Replace("\n", "").Replace("\r", "").Replace(" ", "");

            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            var x = JsonSerializer.Deserialize<context>(content, options);

            if (x.Data is null)
                throw new Exception("data not found");
            int start = GetStartIndex(x.Data, Line1);
            var line1 = x.Data[start..x.Data.IndexOf(Line2)].Trim();
            var line1Splitted = line1.Split('/');
            var start2 = GetStartIndex(x.Data, Line2);
            var line2 = x.Data[start2..(x.Data.Length)];
            var line2Splitted = line2.Split('/');
            Dictionary<string, string> data = new Dictionary<string, string>();

            for (int i = 0; i < line2Splitted.Length; i++)
            {
                data.Add(line1Splitted[i].Trim(), line2Splitted[i].Trim());
            }

            if (line1Splitted.Length != line2Splitted.Length)
                throw new Exception("Number of parameters and values does not match");
            var employeeDto = new InputEmployeeDto()
            {
                FirstName = data[nameof(InputEmployeeDto.FirstName)],
                LastName = data[nameof(InputEmployeeDto.LastName)],
                NationalCode = data[nameof(InputEmployeeDto.NationalCode)],
                Allowance = decimal.Parse(data[nameof(InputEmployeeDto.Allowance)]),
                Transportation = decimal.Parse(data[nameof(InputEmployeeDto.Transportation)]),
                BasicSalary = decimal.Parse(data[nameof(InputEmployeeDto.BasicSalary)]),
                Date = data[nameof(InputEmployeeDto.Date)],
            };
            var addRequest = new AddRequestDto()
            {
                Data = employeeDto,
                OverTimeCalculator = x.OverTimeCalculator

            };
            return await InputFormatterResult.SuccessAsync(addRequest);
        }
        catch
        {
            return await InputFormatterResult.FailureAsync();
        }
    }
    private DateTime GetDateFromString(string str)
    {
        string[] dateFormats = new[] { "yyyyMMdd" };
        CultureInfo provider = new("fa-IR");
        return DateTime.ParseExact(str, dateFormats, provider,
        DateTimeStyles.AdjustToUniversal);
    }

    private static int GetStartIndex(string content, string keyword)
    {
        return content.IndexOf(keyword) + keyword.Length;
    }
}
public class context
{
    public string Data { get; set; }
    public Calculator OverTimeCalculator { get; set; }
}


