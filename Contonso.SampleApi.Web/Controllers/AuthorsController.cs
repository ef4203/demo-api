namespace Contonso.SampleApi.Web.Controllers;

using Contonso.SampleApi.Application.Authors.Commands;
using Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;
using Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;
using Contonso.SampleApi.Application.Authors.Queries.GetAuthors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[ApiController]
[Route("[controller]")]
public class AuthorsController : BaseController
{
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> Index()
    {
        var result = await this.Mediator.Send(new GetAuthorsQuery());

        return this.Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAuthor([FromBody] CreateAuthorCommand command)
    {
        return await this.Mediator.Send(command);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAuthor([FromRoute] Guid id)
    {
        await this.Mediator.Send(new DeleteAuthorCommand(id));

        return this.NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateBook([FromRoute] Guid id,
        [FromBody] UpdateAuthorCommand command)
    {
        if (id != command.Id)
        {
            return this.BadRequest();
        }

        await this.Mediator.Send(command);

        return this.NoContent();
    }
}
