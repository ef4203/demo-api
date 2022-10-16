namespace Contonso.SampleApi.Application.Common.Abstraction;

using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }
    DbSet<Author> Authors { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
