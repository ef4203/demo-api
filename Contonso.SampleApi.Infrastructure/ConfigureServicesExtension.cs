namespace Contonso.SampleApi.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        services.AddDbContext<ApplicationDbContext>(
            o
                => o.UseInMemoryDatabase("ExampleApi"));

        services.AddTransient(typeof(IApplicationDbContext), typeof(ApplicationDbContext));

        return services;
    }
}
