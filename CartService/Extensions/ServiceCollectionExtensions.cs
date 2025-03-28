using CartService.Behaviors;
using CartService.Middlewares;

namespace CartService.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddMarten(cfg => cfg.Connection
            (configuration.GetConnectionString("Database")!))
            .UseLightweightSessions();

        services.AddCarter();
        services.AddScoped<ErrorHandlingMiddleware>();
    }
}
