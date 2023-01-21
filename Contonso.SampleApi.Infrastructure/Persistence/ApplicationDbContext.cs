namespace Contonso.SampleApi.Infrastructure.Persistence;

using System.Runtime.CompilerServices;
using Contonso.SampleApi.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;

    public DbSet<Author> Authors { get; set; } = null!;

    public Task<int> SaveChangesAsync()
    {
        this.ProcessInternalChanges();
        return base.SaveChangesAsync();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        this.ProcessInternalChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureCreationTracking(EntityEntry entry, DateTime now)
    {
        if (entry.Entity is ICreationTracker creationTrackable && entry.State == EntityState.Added)
        {
            creationTrackable.CreatedOn = now;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureModificationTracking(EntityEntry entry, DateTime now)
    {
        if (entry.Entity is IModificationTracker modificationTrackable &&
            (entry.State == EntityState.Modified || entry.State == EntityState.Deleted))
        {
            modificationTrackable.ModifiedOn = now;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureSoftDeletion(EntityEntry entry, bool force = false)
    {
        if (entry.State != EntityState.Deleted || force)
        {
            return;
        }

        if (entry.Entity is not IArchivable archivable)
        {
            return;
        }

        archivable.IsDeleted = true;
        entry.State = EntityState.Modified;
    }

    private void ProcessInternalChanges()
    {
        var utcNow = DateTime.UtcNow;
        var entries = this.ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            if (entry.State != EntityState.Added && entry.State != EntityState.Modified &&
                entry.State != EntityState.Deleted)
            {
                continue;
            }

            EnsureCreationTracking(entry, utcNow);
            EnsureModificationTracking(entry, utcNow);
            EnsureSoftDeletion(entry);
        }
    }
}
