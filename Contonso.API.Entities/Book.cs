namespace Contonso.API.Entities
{
    using System;
    using Contonso.Common.EntityFrameworkCore.Abstraction;

    /// <summary>
    ///     The book entity.
    /// </summary>
    /// <seealso cref="ApplicationEntity" />
    public class Book : ApplicationEntity, ICreationTracker, IModificationTracker, IArchivable
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     Gets or sets the created on.
        /// </summary>
        /// <value>
        ///     The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     Gets or sets the created by.
        /// </summary>
        /// <value>
        ///     The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        ///     Gets or sets the modified on.
        /// </summary>
        /// <value>
        ///     The modified on.
        /// </value>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        ///     Gets or sets the modified by.
        /// </summary>
        /// <value>
        ///     The modified by.
        /// </value>
        public string ModifiedBy { get; set; }

        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        /// <value>
        ///     The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>
        ///     The title.
        /// </value>
        public string Title { get; set; }
    }
}
