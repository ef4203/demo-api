namespace Contonso.API.Common.Data
{
    using System;

    /// <summary>
    /// Indicated if an entity has a creation tracker.
    /// </summary>
    public interface ICreationTracker
    {
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
    }
}