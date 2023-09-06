namespace Contonso.SampleApi.Tests.Application.Common;

using AutoMapper;
using Contonso.SampleApi.Application;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Respawn;

public class BaseTest
{
    protected BaseTest()
    {
        var serviceProvider = new ServiceCollection()
            .ConfigureServices()
            .BuildServiceProvider();

        this.DbContext = serviceProvider.GetRequiredService<IAppDbContext>();
        this.Mapper = serviceProvider.GetRequiredService<IMapper>();
        this.Mediator = serviceProvider.GetRequiredService<ISender>();

        StartRespawner(serviceProvider);
    }

    public IAppDbContext DbContext { get; }

    public IMapper Mapper { get; }

    public ISender Mediator { get; }

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
}

internal static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        services.AddApplicationServices();
        services.AddDbContext<AppDbContext>(
            o =>
                o.UseInMemoryDatabase("ExampleApi"));

        services.AddTransient<IAppDbContext, AppDbContext>();
        services.AddTransient(o => Mock.Of<IJobClient>()!);
        services.AddLogging();

        return services;
    }
}
