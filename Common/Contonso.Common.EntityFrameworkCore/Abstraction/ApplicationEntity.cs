namespace Contonso.Common.EntityFrameworkCore.Abstraction
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     The top level application entity.
    /// </summary>
    public abstract class ApplicationEntity
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        [Key]
        public Guid Id { get; set; }
    }
}
