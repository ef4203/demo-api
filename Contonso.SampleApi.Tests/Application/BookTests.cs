namespace Contonso.SampleApi.Tests.Application;

using AutoMapper;
using Contonso.SampleApi.Application.Books.Commands;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Mapping;
using Contonso.SampleApi.Infrastructure.Persistence;
using Contonso.SampleApi.Tests.Application.Common;
using Microsoft.EntityFrameworkCore;

public class BookTests : BaseTest
{
    [Test]
    public void CreateCommandRequestCannotBeNull()
    {
        var handler = new CreateBookCommandHandler(this.DbContext);

        Assert.That(async () => await handler.Handle(null, CancellationToken.None),
            Throws.InstanceOf<ArgumentNullException>());
    }

    [Test]
    public void DeleteCommandRequestCannotBeNull()
    {
        var handler = new DeleteBookCommandHandler(this.DbContext);

        Assert.That(async () => await handler.Handle(null, CancellationToken.None),
            Throws.InstanceOf<ArgumentNullException>());
    }

    [Test]
    public void UpdateCommandRequestCannotBeNull()
    {
        var handler = new UpdateBookCommandHandler(this.DbContext);

        Assert.That(async () => await handler.Handle(null, CancellationToken.None),
            Throws.InstanceOf<ArgumentNullException>());
    }
}
