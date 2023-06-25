using CalculationOfEmployeeSalaries.Core.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CalculationOfEmployeeSalaries.Infrastructure.DataAccess
{
    public sealed class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
        

            builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();


            builder
               .Property(u => u.NationalCode).HasMaxLength(10).IsUnicode(true).IsRequired();

            builder.OwnsMany<EmployeeSalary>(c => c.EmployeeSalaries, c =>
            {
                c.WithOwner().HasForeignKey(c => c.EmployeeId);
                c.Property(u => u.Allowance).IsRequired();
                c.Property(u => u.BasicSalary).IsRequired();
                c.Property(u => u.Transportation).IsRequired();
                c.Property(u => u.Date).IsRequired();
                c.Property(u => u.Calculator).IsRequired();
                c.Property(e => e.Date).HasConversion<DateTime>();

            });
         
            

        }

       
    }
}
