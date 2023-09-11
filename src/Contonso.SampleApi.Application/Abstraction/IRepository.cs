namespace Contonso.SampleApi.Application.Abstraction;

public interface IRepository<TEntity>
    where TEntity : class
{
    ValueTask<TEntity?> GetAsync(object key, CancellationToken cancellationToken = default);

    Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(object key, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
