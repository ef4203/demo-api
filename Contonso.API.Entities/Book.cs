namespace Contonso.API.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The book.
    /// </summary>
    /// <seealso cref="Contonso.API.Entities.ApplicationEntity" />
    public class Book : ApplicationEntity
    {
        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
    }
}
