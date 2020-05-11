namespace Contonso.API.Services
{
    using Contonso.API.Common.Data;
    using Contonso.API.Data;
    using Contonso.API.Entities;

    /// <summary>
    /// Provides service methods for database operations related to the <see cref="Book"/> entity.
    /// </summary>
    /// <seealso cref="GenericEntityService{TEntity}" />
    public class BookService : GenericEntityService<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BookService(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
