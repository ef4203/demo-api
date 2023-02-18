namespace Contonso.SampleApi.Application;

using System.Reflection;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(ConfigureServicesExtension).Assembly);
        services.AddMediatorFromAssembly(typeof(ConfigureServicesExtension).Assembly);
        services.AddValidatorsFromAssembly(typeof(ConfigureServicesExtension).Assembly);
        services.AddJobsFromAssembly(typeof(ConfigureServicesExtension).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    private static IServiceCollection AddJobsFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));
        _ = assembly ?? throw new ArgumentNullException(nameof(assembly));

        var jobs = assembly
            .GetTypes()
            .Where(x => x.IsClass && x.GetInterface(nameof(IJob)) != null);

        foreach (var jobType in jobs)
        {
            services.AddTransient(jobType);
        }

        return services;
    }

    private static IServiceCollection AddMediatorFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        services.AddMediatR(
            x =>
            {
                x.RegisterServicesFromAssemblies(assembly);
                x.NotificationPublisher = new TaskWhenAllPublisher();
            });

        return services;
    }
}
