namespace Contonso.Common.EntityFrameworkCore
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Contonso.Common.EntityFrameworkCore.Abstraction;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <inheritdoc />
    public class GenericDbContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericDbContext" /> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public GenericDbContext(DbContextOptions options)
            : base(options ?? throw new ArgumentNullException(nameof(options)))
        {
        }

        /// <summary>
        ///     Updates the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="data">The data.</param>
        public void Update(ApplicationEntity target, ApplicationEntity data)
        {
            _ = target ?? throw new ArgumentNullException(nameof(data));
            _ = data ?? throw new ArgumentNullException(nameof(data));

            data.Id = target.Id;
            this.Entry(target)?.CurrentValues?.SetValues(data);
        }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            this.ProcessInternalOperations();
            return base.SaveChanges();
        }

        /// <inheritdoc />
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ProcessInternalOperations();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ProcessInternalOperations();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        ///     Ensures the creation tracking.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="now">The now.</param>
        /// <param name="username">The username.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureCreationTracking(EntityEntry entry, DateTime now, string username)
        {
            if (entry is not null && entry.Entity is ICreationTracker creationTrackable &&
                entry.State == EntityState.Added)
            {
                creationTrackable.CreatedOn = now;
                creationTrackable.CreatedBy = username;
            }
        }

        /// <summary>
        ///     Ensures the modification tracking.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="now">The now.</param>
        /// <param name="username">The username.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureModificationTracking(EntityEntry entry, DateTime now, string username)
        {
            if (entry is not null && entry.Entity is IModificationTracker modificationTrackable && (
                entry.State == EntityState.Modified || entry.State == EntityState.Deleted))
            {
                modificationTrackable.ModifiedOn = now;
                modificationTrackable.ModifiedBy = username;
            }
        }

        /// <summary>
        ///     Ensures the soft deletion.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="force">
        ///     Indicates whether the delete operation will force even though the entry is
        ///     <see cref="IArchivable" />.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureSoftDeletion(EntityEntry entry, bool force = false)
        {
            if (entry is not null && entry.State == EntityState.Deleted && !force)
            {
                if (entry.Entity is IArchivable archivable)
                {
                    archivable.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }

        /// <summary>
        ///     Processes the internal operations.
        /// </summary>
        private void ProcessInternalOperations()
        {
            var utcNow = DateTime.UtcNow;
            var userName = "System";

            foreach (var entry in this.ChangeTracker?.Entries() ?? Array.Empty<EntityEntry>())
            {
                if (entry != null &&
                    (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted))
                {
                    EnsureCreationTracking(entry, utcNow, userName);
                    EnsureModificationTracking(entry, utcNow, userName);
                    EnsureSoftDeletion(entry);
                }
            }
        }
    }
}
