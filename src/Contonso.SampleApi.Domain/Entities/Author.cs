namespace Contonso.SampleApi.Domain.Entities;

using Contonso.SampleApi.Domain.Common;

public class Author : ApplicationEntity<Guid>, ICreationTracker, IModificationTracker, IArchivable
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public ICollection<Book> Books { get; set; } = new HashSet<Book>();

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}
