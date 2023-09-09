namespace Contonso.SampleApi.Infrastructure;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Infrastructure.Persistence;
using Contonso.SampleApi.Infrastructure.Scheduling;
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
        services.AddTransient<IJobClient, JobClient>();

        return services;
    }
}
