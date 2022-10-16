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
    protected readonly IApplicationDbContext DbContext;
    protected readonly IMapper Mapper;
    protected readonly ISender Mediator;
    private readonly IServiceProvider serviceProvider;

    protected BaseTest()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        this.serviceProvider = services.BuildServiceProvider();

        this.DbContext = this.serviceProvider.GetRequiredService<IApplicationDbContext>();
        this.Mapper = this.serviceProvider.GetRequiredService<IMapper>();
        this.Mediator = this.serviceProvider.GetRequiredService<ISender>();
        var logger = this.serviceProvider.GetRequiredService<ILogger>();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddDbContext<ApplicationDbContext>(o =>
            o.UseInMemoryDatabase("ExampleApi"));
        services.AddTransient(typeof(IApplicationDbContext), typeof(ApplicationDbContext));
        services.AddTransient(o => Mock.Of<ILogger>());
        services.AddTransient(o => Mock.Of<ILogger<CreateBookCommand>>());
        services.AddTransient(o => Mock.Of<ILogger<DeleteBookCommand>>());
        services.AddTransient(o => Mock.Of<ILogger<UpdateBookCommand>>());
    }
}
