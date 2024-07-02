using Emi.Employees.Domain.Shared;
using Emi.Employees.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
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
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        InitializeSecurityModel(serviceScope.ServiceProvider, userManager).Wait();
    }

    public static async Task InitializeSecurityModel(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Manager", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var manager = await userManager.FindByEmailAsync("admin@example.com");

        if (manager == null)
        {
            manager = new IdentityUser()
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
            };
            await userManager.CreateAsync(manager, "Colombia123!");
            await userManager.AddToRoleAsync(manager, "Manager");
        }

        var user = await userManager.FindByEmailAsync("user@example.com");

        if (user == null)
        {
            user = new IdentityUser()
            {
                UserName = "user@example.com",
                Email = "user@example.com",
            };
            await userManager.CreateAsync(user, "Colombia111!");
            await userManager.AddToRoleAsync(user, "User");
        }


    }
}
