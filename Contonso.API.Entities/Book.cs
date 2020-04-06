namespace Contonso.API.Entities
{
    using Contonso.API.Common.Entities;

    /// <summary>
    /// The book entity.
    /// </summary>
    /// <seealso cref="ApplicationEntity" />
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
