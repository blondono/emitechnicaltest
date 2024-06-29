using Emi.Employees.Domain.Shared;
using Emi.Employees.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Emi.Employees.Infrastructure;

public static class LayerInjection
{
    public static void InstallInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetValue<string>("ConnectionStrings:EmiDatabase");
        services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(connectionString));
        services.TryAddScoped(typeof(IRepository<>), typeof(EmployeeRepository<>));
    }
    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<EmployeeDbContext>();
        context?.Database.Migrate();
    }
}
