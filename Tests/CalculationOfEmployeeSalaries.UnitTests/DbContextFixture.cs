using CalculationOfEmployeeSalaries.Infrastructure.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace CalculationOfEmployeeSalaries.UnitTests;

public sealed class DbContextFixture : IDbContextFixture
{
    private readonly DbContextOptions<ApplicationContext> _options;

    public ApplicationContext DbContextFake { get; }
    public IDbConnection DbConnectionFake { get; }
    public DbContextFixture()
    {
        _options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite("Data Source=LocalDatabase.db")
            .Options;

        DbContextFake = new ApplicationContext(_options);
        DbContextFake.Database.EnsureCreated();
        DbConnectionFake = new SqlConnection("Data Source=LocalDatabase.db");
    }

    public void Dispose()
    {
        DbContextFake.Database.EnsureDeleted();
        DbContextFake.Dispose();
    }
    public async ValueTask DisposeAsync()
    {
        await DbContextFake.Database.EnsureDeletedAsync();
        await DbContextFake.DisposeAsync();
    }
}