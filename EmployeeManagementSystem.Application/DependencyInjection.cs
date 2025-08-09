using EmployeeManagementSystem.Application.Common;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly);

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ApplicationAssemblyMarker))
                .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
