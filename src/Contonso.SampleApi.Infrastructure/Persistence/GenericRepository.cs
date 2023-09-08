namespace Contonso.SampleApi.Infrastructure.Persistence;

using Contonso.SampleApi.Application.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly AppDbContext dbContext;

    public GenericRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ValueTask<TEntity?> GetAsync(object key, CancellationToken cancellationToken = default)
    {
        return this.dbContext.FindAsync<TEntity>(key);
    }

    public Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return this.dbContext.Set<TEntity>()
            .ToArrayAsync(cancellationToken);
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        this.dbContext.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        this.dbContext.Attach(entity);
        this.dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(object key, CancellationToken cancellationToken = default)
    {
        var entity = await this.dbContext.FindAsync<TEntity>(key);
        if (entity != null)
        {
            this.dbContext.Remove(entity);
        }
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return this.dbContext.SaveChangesAsync(cancellationToken);
    }
}
