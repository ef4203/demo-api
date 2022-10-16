namespace Contonso.SampleApi.Tests.Application.Common;

using AutoMapper;
using Contonso.SampleApi.Application;
using Contonso.SampleApi.Application.Books.Commands.CreateBook;
using Contonso.SampleApi.Application.Books.Commands.DeleteBook;
using Contonso.SampleApi.Application.Books.Commands.UpdateBook;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

    public IApplicationDbContext DbContext { get; private set; }

    public IMapper Mapper { get; private set; }

    public ISender Mediator { get; private set; }
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

        services.AddTransient(typeof(IApplicationDbContext), typeof(ApplicationDbContext));
        services.AddTransient(o => Mock.Of<ILogger>()!);
        services.AddTransient(o => Mock.Of<ILogger<CreateBookCommand>>()!);
        services.AddTransient(o => Mock.Of<ILogger<DeleteBookCommand>>()!);
        services.AddTransient(o => Mock.Of<ILogger<UpdateBookCommand>>()!);

        return services;
    }
}
