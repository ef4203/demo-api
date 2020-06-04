namespace Contonso.API.Common.Data
{
    using System;

    /// <summary>
    /// Indicates if the entity has an modification tracker.
    /// </summary>
    public interface IModificationTracker
    {
        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public string ModifiedBy { get; set; }
    }
}