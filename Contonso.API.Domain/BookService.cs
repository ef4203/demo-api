namespace Contonso.API.Domain
{
    using Contonso.API.Data;
    using Contonso.API.Entities;
    using Contonso.Common.EntityFrameworkCore;

    /// <inheritdoc />
    public class BookService : GenericEntityService<Book>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BookService" /> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BookService(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
