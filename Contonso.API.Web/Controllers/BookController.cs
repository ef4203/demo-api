namespace Contonso.API.Web.Controllers
{
    using Contonso.API.Entities;
    using Contonso.API.Services;
    using Contonso.API.Web.Controllers.Generic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Provides endpoints for books.
    /// </summary>
    [Route("api/books")]
    [ApiController]
    public class BookController : GenericController<BookService, Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public BookController(BookService service)
            : base(service)
        {
        }
    }
}