namespace Contonso.SampleApi.Tests.Application;

using Bogus.DataSets;
using Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;
using Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;
using Contonso.SampleApi.Application.Authors.Commands.UpdateAuthor;
using Contonso.SampleApi.Application.Authors.Queries.GetAuthors;
using Contonso.SampleApi.Tests.Application.Common;

public class AuthorTests : BaseTest
{
    [Test]
    public void CreateAuthorCommandRCannotBeNull()
    {
        Assert.Throws<ArgumentNullException>(
            () =>
                this.Mediator.Send((CreateAuthorCommand)null));
    }

    [Test]
    public void DeleteAuthorCommandCannotBeNull()
    {
        Assert.Throws<ArgumentNullException>(
            () =>
                this.Mediator.Send((DeleteAuthorCommand)null));
    }

    [Test]
    public void UpdateAuthorCommandCannotBeNull()
    {
        Assert.Throws<ArgumentNullException>(
            () =>
                this.Mediator.Send((UpdateAuthorCommand)null));
    }

    [Test]
    public void GetAuthorQueryCannotBeNull()
    {
        Assert.Throws<ArgumentNullException>(
            () =>
                this.Mediator.Send((GetAuthorsQuery)null));
    }

    [Test]
    public async Task CreateAuthor()
    {
        var nameDataSet = new Name();

        var command = new CreateAuthorCommand
        {
            FirstName = nameDataSet.FirstName(),
            LastName = nameDataSet.LastName(),
        };

        await this.Mediator.Send(command);
    }

    [Test]
    public async Task FirstNameCannotBeNullOnCreate()
    {
        var nameDataSet = new Name();

        var command = new CreateAuthorCommand
        {
            FirstName = null,
            LastName = nameDataSet.LastName(),
        };

        await this.Mediator.Send(command);
    }

    [Test]
    public async Task FirstNameCannotBeEmptyStringOnCreate()
    {
        var nameDataSet = new Name();

        var command = new CreateAuthorCommand
        {
            FirstName = string.Empty,
            LastName = nameDataSet.LastName(),
        };

        await this.Mediator.Send(command);
    }

    [Test]
    public async Task LastNameCannotBeNullOnCreate()
    {
        var nameDataSet = new Name();

        var command = new CreateAuthorCommand
        {
            FirstName = nameDataSet.FirstName(),
            LastName = null,
        };

        await this.Mediator.Send(command);
    }

    [Test]
    public async Task LastNameCannotBeEmptyStringOnCreate()
    {
        var nameDataSet = new Name();

        var command = new CreateAuthorCommand
        {
            FirstName = nameDataSet.FirstName(),
            LastName = string.Empty,
        };

        await this.Mediator.Send(command);
    }

    [Test]
    public async Task DeleteAuthor()
    {
        var nameDataSet = new Name();

        var command = new CreateAuthorCommand
        {
            FirstName = nameDataSet.FirstName(),
            LastName = nameDataSet.LastName(),
        };

        var id = await this.Mediator.Send(command);
        Assert.That(id, Is.Not.Empty);
        await this.Mediator.Send(new DeleteAuthorCommand(id));
    }

    [Test]
    public async Task UpdateAuthor()
    {
        var nameDataSet = new Name();

        var createCommand = new CreateAuthorCommand
        {
            FirstName = nameDataSet.FirstName(),
            LastName = nameDataSet.LastName(),
        };

        var id = await this.Mediator.Send(createCommand);
        Assert.That(id, Is.Not.Empty);

        var updateCommand = new UpdateAuthorCommand
        {
            Id = id,
            FirstName = nameDataSet.FirstName(),
            LastName = nameDataSet.LastName(),
        };

        await this.Mediator.Send(updateCommand);
    }
}
