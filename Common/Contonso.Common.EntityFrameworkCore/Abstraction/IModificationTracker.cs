namespace Contonso.Common.EntityFrameworkCore.Abstraction
{
    using System;

    /// <summary>
    ///     Indicates if the entity has an modification tracker.
    /// </summary>
    public interface IModificationTracker
    {
        /// <summary>
        ///     Gets or sets the modified on.
        /// </summary>
        /// <value>
        ///     The modified on.
        /// </value>
        DateTime ModifiedOn { get; set; }

        /// <summary>
        ///     Gets or sets the modified by.
        /// </summary>
        /// <value>
        ///     The modified by.
        /// </value>
        string ModifiedBy { get; set; }
    }
}
