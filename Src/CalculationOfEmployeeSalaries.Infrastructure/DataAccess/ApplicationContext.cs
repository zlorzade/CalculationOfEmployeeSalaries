
using CalculationOfEmployeeSalaries.Core.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data;
using System.Reflection;

namespace CalculationOfEmployeeSalaries.Infrastructure.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public IDbConnection Connection => Database.GetDbConnection();
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSalary> Salaries { get; set; }


    }
   

}