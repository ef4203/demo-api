namespace Contonso.SampleApi.Application;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        services.AddMediatR(typeof(ConfigureServicesExtension).Assembly);
        services.AddAutoMapper(typeof(ConfigureServicesExtension).Assembly);
        services.AddValidatorsFromAssembly(typeof(ConfigureServicesExtension).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
