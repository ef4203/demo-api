namespace Contonso.SampleApi.Domain.Entities;

public class Book : ApplicationEntity<Guid>, ICreationTracker, IModificationTracker, IArchivable
{
    public string? Title { get; set; }
    public Author? Author { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime PublishDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}
