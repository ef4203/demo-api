namespace Contonso.SampleApi.Tests.Application;

using Bogus;
using Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;
using Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;
using Contonso.SampleApi.Application.Authors.Commands.UpdateAuthor;
using Contonso.SampleApi.Application.Authors.Queries.GetAuthors;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Tests.Application.Common;
using ValidationException = Contonso.SampleApi.Application.Common.Exceptions.ValidationException;

public class AuthorTests : BaseTest
{
    private readonly Faker<CreateAuthorCommand> createAuthorCommandFaker;

    private readonly Faker<UpdateAuthorCommand> updateAuthorCommandFaker;

    public AuthorTests()
    {
        this.updateAuthorCommandFaker = new Faker<UpdateAuthorCommand>()
            .RuleFor(o => o.Id, f => f.Random.Guid())
            .RuleFor(o => o.FirstName, f => f.Name.FirstName())
            .RuleFor(o => o.LastName, f => f.Name.LastName());

        this.createAuthorCommandFaker = new Faker<CreateAuthorCommand>()
            .RuleFor(o => o.FirstName, f => f.Name.FirstName())
            .RuleFor(o => o.LastName, f => f.Name.LastName());
    }

    [Test]
    public void CreateAuthorCommandRCannotBeNull()
    {
        var action =
            async () => await this.Mediator.Send((CreateAuthorCommand)null!);

        action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public void DeleteAuthorCommandCannotBeNull()
    {
        var action =
            async () => await this.Mediator.Send((DeleteAuthorCommand)null!);

        action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public void UpdateAuthorCommandCannotBeNull()
    {
        var action =
            async () => await this.Mediator.Send((UpdateAuthorCommand)null!);

        action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public void GetAuthorQueryCannotBeNull()
    {
        var action =
            async () => await this.Mediator.Send((GetAuthorsQuery)null!);

        action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task CreateAuthor()
    {
        var command = this.createAuthorCommandFaker.Generate();
        var result = await this.Mediator.Send(command);

        result.Should().NotBeEmpty();
    }

    [Test]
    public async Task FirstNameCannotBeNullOnCreate()
    {
        var command = this.createAuthorCommandFaker.Generate();
        command.FirstName = null;

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task FirstNameCannotBeEmptyStringOnCreate()
    {
        var command = this.createAuthorCommandFaker.Generate();
        command.FirstName = string.Empty;

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task LastNameCannotBeNullOnCreate()
    {
        var command = this.createAuthorCommandFaker.Generate();
        command.LastName = null;

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task LastNameCannotBeEmptyStringOnCreate()
    {
        var command = this.createAuthorCommandFaker.Generate();
        command.LastName = string.Empty;

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task DeleteAuthor()
    {
        var command = this.createAuthorCommandFaker.Generate();
        var id = await this.Mediator.Send(command);

        id.Should().NotBeEmpty();

        var action =
            async () => await this.Mediator.Send(new DeleteAuthorCommand(id));

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateAuthor()
    {
        var createCommand = this.createAuthorCommandFaker.Generate();
        var id = await this.Mediator.Send(createCommand);

        id.Should().NotBeEmpty();

        var updateCommand = this.updateAuthorCommandFaker.Generate();
        updateCommand.Id = id;

        var action =
            async () => await this.Mediator.Send(updateCommand);

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task CannotUpdateNonExistingAuthor()
    {
        var updateCommand = this.updateAuthorCommandFaker.Generate();

        var action =
            async () => await this.Mediator.Send(updateCommand);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task CannotDeleteNonExistingAuthor()
    {
        var action =
            async () => await this.Mediator.Send(new DeleteAuthorCommand(Guid.NewGuid()));

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task GetAuthors()
    {
        for (var i = 0; i < 10; i++)
        {
            var command = this.createAuthorCommandFaker.Generate();
            await this.Mediator.Send(command);
        }

        var result =
            await this.Mediator.Send(new GetAuthorsQuery());

        result.Should().NotBeEmpty().And.NotBeNull();
    }
}
