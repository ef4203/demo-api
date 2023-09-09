namespace Contonso.SampleApi.Tests.Application.Common;

using Contonso.SampleApi.Application;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Respawn;

public class BaseTest
{
    private readonly ServiceProvider serviceProvider;

    protected BaseTest()
    {
        this.serviceProvider = ConfigureServices(new ServiceCollection())
            .BuildServiceProvider();

        StartRespawner(this.serviceProvider);
    }

    protected T Get<T>()
        where T : notnull
    {
        return this.serviceProvider.GetRequiredService<T>();
    }

    private static void StartRespawner(IServiceProvider service)
    {
        var ctx = service.GetRequiredService<AppDbContext>();
        if (!ctx.Database.IsRelational())
        {
            return;
        }

        var respawn = Respawner.CreateAsync(ctx.Database.GetDbConnection());
        respawn.RunSynchronously();
    }

    private static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        services.AddApplicationServices();
        services.AddDbContext<AppDbContext>(
            o =>
                o.UseInMemoryDatabase("ExampleApi"));

        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddTransient(_ => Mock.Of<IJobClient>()!);
        services.AddLogging();

        return services;
    }
}
