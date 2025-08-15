using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(LoggingBehavior<,>), typeof(ValidationBehavior<,>));
            });
        return services;
    }
}
