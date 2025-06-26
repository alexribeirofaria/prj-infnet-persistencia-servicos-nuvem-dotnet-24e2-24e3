using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixCharge.Repository.DataSeeders;

namespace PixCharge.Repository.DependencyInjection;
public static class DataBaseDependencyInjection
{
    public static void CreateDataBaseMsSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RegisterContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("MsSqlServerConnectionString"),
            b => b.MigrationsAssembly("PixCharge.Infrastructure.Migrations_MsSqlServer")));
        services.AddTransient<IDataSeeder, DataSeeder>();

    }
    public static void CreateDataBaseInMemory(this IServiceCollection services)
    {
        services.AddDbContext<RegisterContext>(c => c.UseInMemoryDatabase("Register"));
        services.AddTransient<IDataSeeder, DataSeeder>();
    }
}