namespace Contonso.SampleApi.Infrastructure;

using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        services.AddDbContext<ApplicationDbContext>(
            o => o
                .UseInMemoryDatabase("ExampleApi"));

        services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

        services.AddHangfire(
            configuration => configuration
                .UseSimpleAssemblyNameTypeSerializer()?
                .UseRecommendedSerializerSettings()?
                .UseMemoryStorage());

        services.AddHangfireServer();
        services.AddTransient<IApplicationBackgroundJobService, ApplicationBackgroundJobService>();

        return services;
    }
}
