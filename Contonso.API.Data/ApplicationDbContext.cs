namespace Contonso.API.Data
{
    using System;
    using Contonso.API.Entities;
    using Contonso.Common.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    public class ApplicationDbContext : GenericDbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions options)
            : base(options ?? throw new ArgumentNullException(nameof(options)))
        {
        }

        /// <summary>
        ///     Gets or sets the books.
        /// </summary>
        /// <value>
        ///     The books.
        /// </value>
        public DbSet<Book> Books { get; set; }
    }
}
