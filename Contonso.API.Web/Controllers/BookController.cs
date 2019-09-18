namespace Contonso.API.Web.Controllers
{
    using Contonso.API.Entities;
    using Contonso.API.Services;
    using Contonso.API.Web.Controllers.Generic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Provides HTTP endpoints for the <see cref="Book"/> entity.
    /// </summary>
    /// <seealso cref="GenericEntityController{BookService, Book}" />
    [ApiController]
    [Route("api/books")]
    public class BookController : GenericEntityController<BookService, Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="service">The book service.</param>
        public BookController(BookService service)
            : base(service)
        {
        }
    }
}