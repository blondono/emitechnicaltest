using Emi.Employees.Application.MapperProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emi.Employees.Application
{
    public static class LayerInjection
    {
        public static IServiceCollection InstallApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMappingPipeline();
            services.ConfigureAutoMapper();
            return services;
        }
        private static void ConfigureMappingPipeline(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LayerInjection).Assembly));
        }
        private static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
            typeof(EmployeeProfile) 
            );
        }
    }
}
