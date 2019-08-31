namespace Contonso.API.Services
{
    using Contonso.API.Data;
    using Contonso.API.Entities;
    using Contonso.API.Services.Generic;

    /// <summary>
    /// A service for book related actions on the database.
    /// </summary>
    /// <seealso cref="Contonso.API.Services.Generic.GenericEntityService{Book}" />
    public class BookService : GenericEntityService<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BookService(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
