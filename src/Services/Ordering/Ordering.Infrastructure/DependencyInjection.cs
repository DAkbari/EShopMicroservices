using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Application.Data;
using Ordering.Infrastructure.Interceptors;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }
}
