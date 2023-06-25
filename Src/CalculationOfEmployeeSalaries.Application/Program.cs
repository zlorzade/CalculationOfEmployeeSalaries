using CalculationOfEmployeeSalaries.Application;
using CalculationOfEmployeeSalaries.Infrastructure.DataAccess;
using System.Text.Json;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjection(builder.Configuration);

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        });


builder.Services.AddSwaggerGen();


builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new CustomInputFormatter());
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
});

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.Run();
