namespace Contonso.Common.EntityFrameworkCore.Abstraction
{
    /// <summary>
    ///     Indicated if an entity is archivable.
    /// </summary>
    public interface IArchivable
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }
    }
}
