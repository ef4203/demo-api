namespace Contonso.API.Web.Controllers
{
    using Contonso.API.Domain;
    using Contonso.API.Entities;
    using Contonso.Common.AspNetCore;
    using Microsoft.AspNetCore.Mvc;

    /// <inheritdoc />
    [ApiController]
    [Route("api/books")]
    public class BookController : GenericEntityController<BookService, Book>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BookController" /> class.
        /// </summary>
        /// <param name="service">The book service.</param>
        public BookController(BookService service)
            : base(service)
        {
        }
    }
}
