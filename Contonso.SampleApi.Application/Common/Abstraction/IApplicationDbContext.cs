namespace Contonso.SampleApi.Application.Common.Abstraction;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }

    DbSet<Author> Authors { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken);
}