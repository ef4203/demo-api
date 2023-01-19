namespace Contonso.SampleApi.Tests.Application.Common;

using AutoMapper;
using Contonso.SampleApi.Application;
using Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;
using Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;
using Contonso.SampleApi.Application.Authors.Commands.UpdateAuthor;
using Contonso.SampleApi.Application.Authors.Events;
using Contonso.SampleApi.Application.Authors.Queries.GetAuthors;
using Contonso.SampleApi.Application.Books.Commands.CreateBook;
using Contonso.SampleApi.Application.Books.Commands.DeleteBook;
using Contonso.SampleApi.Application.Books.Commands.UpdateBook;
using Contonso.SampleApi.Application.Books.Queries.GetBooks;
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
        services.AddTransient(o => Mock.Of<ILogger>()!);
        services.AddTransient(o => Mock.Of<ILogger<CreateBookCommand>>()!);
        services.AddTransient(o => Mock.Of<ILogger<DeleteBookCommand>>()!);
        services.AddTransient(o => Mock.Of<ILogger<UpdateBookCommand>>()!);
        services.AddTransient(o => Mock.Of<ILogger<GetBooksQuery>>()!);
        services.AddTransient(o => Mock.Of<ILogger<CreateAuthorCommand>>()!);
        services.AddTransient(o => Mock.Of<ILogger<UpdateAuthorCommand>>()!);
        services.AddTransient(o => Mock.Of<ILogger<DeleteAuthorCommand>>()!);
        services.AddTransient(o => Mock.Of<ILogger<GetAuthorsQuery>>()!);
        services.AddTransient(o => Mock.Of<ILogger<AuthorCreatedEvent>>()!);

        return services;
    }
}
