namespace Contonso.API.Data
{
    using Contonso.API.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The database context for the application.
    /// </summary>
    /// <seealso cref="DbContext" />
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public DbSet<Book> Books { get; set; }
    }
}
