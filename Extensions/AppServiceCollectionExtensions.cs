using System.Reflection;

namespace TaskMonitor.Extensions;

public static class AppServiceCollectionExtensions
{
    public static IServiceCollection AddAppScopedService(this IServiceCollection services)
    {
        // Register services annotated with ScopedServiceAttribute
        var serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<ScopedServiceAttribute>() != null);

        foreach (var serviceType in serviceTypes)
        {
            foreach (var @interface in serviceType.GetInterfaces())
            {
                var attribute = @interface.GetCustomAttribute<IScopedServiceAttribute>();
                if (attribute != null || @interface.Name == "I" + serviceType.Name)
                {
                    services.AddScoped(@interface, serviceType);
                }
            }
        }

        return services;
    }
}