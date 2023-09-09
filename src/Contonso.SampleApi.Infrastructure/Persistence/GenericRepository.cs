namespace Contonso.SampleApi.Infrastructure.Persistence;

using Contonso.SampleApi.Application.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly AppDbContext appDbContext;

    public GenericRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
    }

    public ValueTask<TEntity?> GetAsync(object key, CancellationToken cancellationToken = default)
    {
        return this.appDbContext.FindAsync<TEntity>(key);
    }

    public Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return this.appDbContext.Set<TEntity>()
            .ToArrayAsync(cancellationToken);
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        this.appDbContext.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        this.appDbContext.Attach(entity);
        this.appDbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(object key, CancellationToken cancellationToken = default)
    {
        var entity = await this.appDbContext.FindAsync<TEntity>(key);
        if (entity != null)
        {
            this.appDbContext.Remove(entity);
        }
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return this.appDbContext.SaveChangesAsync(cancellationToken);
    }
}
