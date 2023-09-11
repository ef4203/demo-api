namespace Contonso.SampleApi.Tests.Application;

using Bogus;
using Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;
using Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;
using Contonso.SampleApi.Application.Authors.Commands.UpdateAuthor;
using Contonso.SampleApi.Application.Authors.Queries.GetAuthors;
using Contonso.SampleApi.Application.Exceptions;
using Contonso.SampleApi.Tests.Application.Common;
using MediatR;
using ValidationException = Contonso.SampleApi.Application.Exceptions.ValidationException;

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

    private IMediator Mediator
    {
        get => this.Get<IMediator>();
    }

    [Test]
    public async Task CreateAuthorCommandRCannotBeNullAsync()
    {
        var action =
            () => this.Mediator.Send((CreateAuthorCommand)null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task DeleteAuthorCommandCannotBeNullAsync()
    {
        var action =
            () => this.Mediator.Send((DeleteAuthorCommand)null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task UpdateAuthorCommandCannotBeNullAsync()
    {
        var action =
            () => this.Mediator.Send((UpdateAuthorCommand)null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task GetAuthorQueryCannotBeNullAsync()
    {
        var action =
            () => this.Mediator.Send((GetAuthorsQuery)null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task CreateAuthorAsync()
    {
        var command = this.createAuthorCommandFaker.Generate();
        var result = await this.Mediator.Send(command);

        result.Should().NotBeEmpty();
    }

    [Test]
    public async Task FirstNameCannotBeNullOnCreateAsync()
    {
        var command = this.createAuthorCommandFaker.Generate();
        command.FirstName = null;

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task FirstNameCannotBeEmptyStringOnCreateAsync()
    {
        var command = this.createAuthorCommandFaker.Generate();
        command.FirstName = string.Empty;

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task LastNameCannotBeNullOnCreateAsync()
    {
        var command = this.createAuthorCommandFaker.Generate();
        command.LastName = null;

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task LastNameCannotBeEmptyStringOnCreateAsync()
    {
        var command = this.createAuthorCommandFaker.Generate();
        command.LastName = string.Empty;

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task DeleteAuthorAsync()
    {
        var command = this.createAuthorCommandFaker.Generate();
        var id = await this.Mediator.Send(command);

        id.Should().NotBeEmpty();

        var action =
            () => this.Mediator.Send(new DeleteAuthorCommand(id));

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateAuthorAsync()
    {
        var createCommand = this.createAuthorCommandFaker.Generate();
        var id = await this.Mediator.Send(createCommand);

        id.Should().NotBeEmpty();

        var updateCommand = this.updateAuthorCommandFaker.Generate();
        updateCommand.Id = id;

        var action =
            () => this.Mediator.Send(updateCommand);

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task CannotUpdateNonExistingAuthorAsync()
    {
        var updateCommand = this.updateAuthorCommandFaker.Generate();

        var action =
            () => this.Mediator.Send(updateCommand);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task CannotDeleteNonExistingAuthorAsync()
    {
        var action =
            () => this.Mediator.Send(new DeleteAuthorCommand(Guid.NewGuid()));

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task GetAuthorsAsync()
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
