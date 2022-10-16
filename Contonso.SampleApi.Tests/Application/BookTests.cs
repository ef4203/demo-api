namespace Contonso.SampleApi.Tests.Application;

using Bogus.DataSets;
using Contonso.SampleApi.Application.Books.Commands.CreateBook;
using Contonso.SampleApi.Application.Books.Commands.DeleteBook;
using Contonso.SampleApi.Application.Books.Commands.UpdateBook;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Tests.Application.Common;

public class BookTests : BaseTest
{
    [Test]
    public void CreateBookCommandRCannotBeNull()
    {
        Assert.Throws<ArgumentNullException>(
            () =>
                this.Mediator.Send((CreateBookCommand)null));
    }

    [Test]
    public void DeleteBookCommandCannotBeNull()
    {
        Assert.Throws<ArgumentNullException>(
            () =>
                this.Mediator.Send((DeleteBookCommand)null));
    }

    [Test]
    public void UpdateBookCommandCannotBeNull()
    {
        Assert.Throws<ArgumentNullException>(
            () =>
                this.Mediator.Send((UpdateBookCommand)null));
    }

    [Test]
    public void BookPublishDateCannotBeInTheFutureOnCreate()
    {
        var commerceDataSet = new Commerce();

        var command = new CreateBookCommand
        {
            Title = commerceDataSet.ProductName(),
            AuthorId = Guid.NewGuid(),
            PublishDate = DateTime.Now.AddYears(10),
        };

        Assert.ThrowsAsync<ValidationException>(
            async () =>
                await this.Mediator.Send(command));
    }

    [Test]
    public void BookPublishDateCannotBeNullOnCreate()
    {
        var commerceDataSet = new Commerce();

        var command = new CreateBookCommand
        {
            Title = commerceDataSet.ProductName(),
            AuthorId = Guid.NewGuid(),
        };

        Assert.ThrowsAsync<ValidationException>(async () => await this.Mediator.Send(command));
    }

    [Test]
    public void BookTitleCannotBeNullOnCreate()
    {
        var command = new CreateBookCommand
        {
            Title = null,
            AuthorId = Guid.NewGuid(),
            PublishDate = DateTime.Now.AddYears(-10),
        };

        Assert.ThrowsAsync<ValidationException>(async () => await this.Mediator.Send(command));
    }

    [Test]
    public void BookTitleCannotBeEmptyStringOnCreate()
    {
        var command = new CreateBookCommand
        {
            Title = string.Empty,
            AuthorId = Guid.NewGuid(),
            PublishDate = DateTime.Now.AddYears(-10),
        };

        Assert.ThrowsAsync<ValidationException>(async () => await this.Mediator.Send(command));
    }

    [Test]
    public void BookAuthorIdCannotBeNullOnCreate()
    {
        var commerceDataSet = new Commerce();

        var command = new CreateBookCommand
        {
            Title = commerceDataSet.ProductName(),
            AuthorId = Guid.Empty,
            PublishDate = DateTime.Now.AddYears(-10),
        };

        Assert.ThrowsAsync<ValidationException>(async () => await this.Mediator.Send(command));
    }

    [Test]
    public async Task DeleteBookCommandThrowsNoException()
    {
        var commerceDataSet = new Commerce();

        var command = new CreateBookCommand
        {
            Title = commerceDataSet.ProductName(),
            AuthorId = Guid.NewGuid(),
            PublishDate = DateTime.Now.AddYears(-10),
        };

        var id = await this.Mediator.Send(command);
        Assert.That(id, Is.Not.Empty);
        await this.Mediator.Send(new DeleteBookCommand(id));
    }

    [Test]
    public async Task CreateBookCommandThrowsNoException()
    {
        var commerceDataSet = new Commerce();

        var command = new CreateBookCommand
        {
            Title = commerceDataSet.ProductName(),
            AuthorId = Guid.NewGuid(),
            PublishDate = DateTime.Now.AddYears(-10),
        };

        var id = await this.Mediator.Send(command);
        Assert.That(id, Is.Not.Empty);
    }

    [Test]
    public async Task UpdateBookCommandThrowsNoException()
    {
        var commerceDataSet = new Commerce();

        var createCommand = new CreateBookCommand
        {
            Title = commerceDataSet.ProductName(),
            AuthorId = Guid.NewGuid(),
            PublishDate = DateTime.Now.AddYears(-10),
        };

        var id = await this.Mediator.Send(createCommand);
        Assert.That(id, Is.Not.Empty);

        var updateCommand = new UpdateBookCommand
        {
            Id = id,
            Title = commerceDataSet.ProductName(),
            AuthorId = Guid.NewGuid(),
            PublishDate = DateTime.Now.AddYears(-10),
        };

        await this.Mediator.Send(updateCommand);
    }
}
