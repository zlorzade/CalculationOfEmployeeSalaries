using CalculationOfEmployeeSalaries.Infrastructure.DataAccess;
using System;

namespace CalculationOfEmployeeSalaries.UnitTests;

public interface IDbContextFixture : IAsyncDisposable, IDisposable
{
    ApplicationContext DbContextFake { get; }
}
