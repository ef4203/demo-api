namespace Contonso.API.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The top level application entity.
    /// </summary>
    public class ApplicationEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public Guid ID { get; set; }
    }
}
