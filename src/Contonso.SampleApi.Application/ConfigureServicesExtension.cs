namespace Contonso.SampleApi.Application;

using System.Reflection;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Behaviors;
using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddMediatorFromAssembly(typeof(ConfigureServicesExtension).Assembly);
        services.AddValidatorsFromAssembly(typeof(ConfigureServicesExtension).Assembly);
        services.AddJobsFromAssembly(typeof(ConfigureServicesExtension).Assembly);

        return services;
    }

    private static IServiceCollection AddJobsFromAssembly(this IServiceCollection services, Assembly assembly)
    {
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
        services.AddMediatR(
            x =>
            {
                x.RegisterServicesFromAssemblies(assembly);
                x.AddOpenBehavior(typeof(LoggingBehavior<,>));
                x.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                x.AddOpenBehavior(typeof(ValidationBehavior<,>));
                x.AddOpenBehavior(typeof(StopwatchBehavior<,>));
                x.NotificationPublisher = new TaskWhenAllPublisher();
            });

        return services;
    }
}
