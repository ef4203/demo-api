namespace Contonso.SampleApi.Infrastructure;

using Contonso.SampleApi.Application.Abstraction;
using Contonso.SampleApi.Infrastructure.Persistence;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<AppDbContext>(
            o => o
                .UseInMemoryDatabase("ExampleApi"));

        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

        services.AddHangfire(
            configuration => configuration
                .UseSimpleAssemblyNameTypeSerializer()
                ?.UseRecommendedSerializerSettings()
                ?.UseSerializerSettings(
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    })
                ?.UseMemoryStorage());

        services.AddHangfireServer();

        return services;
    }
}
