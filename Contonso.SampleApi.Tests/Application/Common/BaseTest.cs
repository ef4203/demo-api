namespace Contonso.SampleApi.Tests.Application.Common;

using AutoMapper;
using Contonso.SampleApi.Application;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

public class BaseTest
{
    protected BaseTest()
    {
        var serviceProvider = new ServiceCollection()
            .ConfigureServices()
            .BuildServiceProvider();

        this.DbContext = serviceProvider.GetRequiredService<IApplicationDbContext>();
        this.Mapper = serviceProvider.GetRequiredService<IMapper>();
        this.Mediator = serviceProvider.GetRequiredService<ISender>();
    }

    public IApplicationDbContext DbContext { get; }

    public IMapper Mapper { get; }

    public ISender Mediator { get; }
}

internal static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));

        services.AddApplicationServices();
        services.AddDbContext<ApplicationDbContext>(
            o =>
                o.UseInMemoryDatabase("ExampleApi"));

        services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient(o => Mock.Of<IApplicationBackgroundJobService>()!);
        services.AddLogging();

        return services;
    }
}