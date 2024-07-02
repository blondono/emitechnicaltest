using Emi.Employees.Domain.Entities;
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
    public static async Task MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<EmployeeDbContext>();
            context?.Database.Migrate();

            await LoadBusinessData(scope, context);
            await LoadAuthenticationData(scope, context);
        }
    }
    public static async Task LoadBusinessData(IServiceScope scope, EmployeeDbContext context)
    {
        var departments = context.Departments.ToList();
        if (!departments.Any())
        {
            context.Departments.AddRange(new List<Department>()
               {
                   new() { Name = "Accounting"},
                   new() { Name = "Administration"},
                   new() { Name = "TI"},
               });

        }
        var projects = context.Projects.ToList();
        if (!projects.Any())
        {
            context.Projects.AddRange(new List<Project>()
               {
                   new() { Name = "R&D"},
                   new() { Name = "AI"},
                   new() { Name = "Payroll"},
               });
        }
        await context.SaveChangesAsync();
    }

    public static async Task LoadAuthenticationData(IServiceScope scope, EmployeeDbContext context)
    {
        var userManagerService = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
        var positionManagerService = scope.ServiceProvider.GetRequiredService<RoleManager<Position>>();

        string[] positions = new string[] { "Manager", "User" };
        foreach (var position in positions)
        {
            if (!context.Position.Any(r => r.Name == position))
            {
                await positionManagerService.CreateAsync(new Position() { Name = position });
            }
        }

        string password = "Password123!";
        var manager = await userManagerService.FindByEmailAsync("manager@company.com");
        if (manager == null)
        {
            var position = await positionManagerService.FindByNameAsync("Manager");
            manager = new Employee
            {
                UserName = "manager@company.com",
                Email = "manager@company.com",
                Name = "User Manager",
                DepartmentId = 1,
                ProjectId = 1,
                Salary = 200,
                PositionId = position.Id
            };
            await userManagerService.CreateAsync(manager, password);
        }

        var user = await userManagerService.FindByEmailAsync("user@company.com");
        if (user == null)
        {
            var position = await positionManagerService.FindByNameAsync("User");
            user = new Employee
            {
                UserName = "user@company.com",
                Email = "user@company.com",
                Name = "Current User",
                DepartmentId = 2,
                ProjectId = 2,
                Salary = 100,
                PositionId = position.Id
            };
            await userManagerService.CreateAsync(user, password);
        }
        await context.SaveChangesAsync();

        var history = context.PositionHistories.ToList();
        if (!history.Any())
        {
            context.PositionHistories.AddRange(new List<PositionHistory>()
            {
                new() { DepartmentId = 1, EmployeeId = 1, PositionId = 1, ProjectId = 1, StartDate = DateTime.UtcNow },
                new() { DepartmentId = 2, EmployeeId = 2, PositionId = 2, ProjectId = 2, StartDate = DateTime.UtcNow },
            });
            await context.SaveChangesAsync();
        }
    }
}
