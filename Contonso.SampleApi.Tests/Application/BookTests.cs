namespace Contonso.SampleApi.Tests.Application;

using AutoMapper;
using Contonso.SampleApi.Application.Books.Commands;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Mapping;
using Contonso.SampleApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class BookTests
{
    private readonly IApplicationDbContext dbContext;

    private readonly IMapper mapper;

    public BookTests()
    {
        var mapperConfiguration =
            new MapperConfiguration(config => config.AddProfile<MappingProfile>());

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("ExampleApi").Options;

        this.mapper = mapperConfiguration.CreateMapper();
        this.dbContext = new ApplicationDbContext(dbContextOptions);
    }

    [Test]
    public void CreateCommandRequestCannotBeNull()
    {
        var handler = new CreateBookCommandHandler(this.dbContext);

        Assert.That(async () => await handler.Handle(null, CancellationToken.None),
            Throws.InstanceOf<ArgumentNullException>());
    }

    [Test]
    public void DeleteCommandRequestCannotBeNull()
    {
        var handler = new DeleteBookCommandHandler(this.dbContext);

        Assert.That(async () => await handler.Handle(null, CancellationToken.None),
            Throws.InstanceOf<ArgumentNullException>());
    }

    [Test]
    public void UpdateCommandRequestCannotBeNull()
    {
        var handler = new UpdateBookCommandHandler(this.dbContext);

        Assert.That(async () => await handler.Handle(null, CancellationToken.None),
            Throws.InstanceOf<ArgumentNullException>());
    }
}