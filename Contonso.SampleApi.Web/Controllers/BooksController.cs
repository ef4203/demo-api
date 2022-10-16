namespace Contonso.SampleApi.Web.Controllers;

using Contonso.SampleApi.Application.Books.Commands.CreateBook;
using Contonso.SampleApi.Application.Books.Commands.DeleteBook;
using Contonso.SampleApi.Application.Books.Commands.UpdateBook;
using Contonso.SampleApi.Application.Books.Queries.GetBooks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[ApiController]
[Route("[controller]")]
public class BooksController : BaseController
{
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<BookDto>>> Index()
    {
        var result = await this.Mediator.Send(new GetBooksQuery());
        return this.Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateBook([FromBody] CreateBookCommand command)
    {
        return await this.Mediator.Send(command);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteBook([FromRoute] Guid id)
    {
        await this.Mediator.Send(new DeleteBookCommand(id));
        return this.NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateBook([FromRoute] Guid id,
        [FromBody] UpdateBookCommand command)
    {
        if (id != command?.Id)
        {
            return this.BadRequest();
        }

        await this.Mediator.Send(command);

        return this.NoContent();
    }
}
