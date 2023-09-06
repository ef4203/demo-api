namespace Contonso.SampleApi.Application.Common.Abstraction;

using System.Data;
using Contonso.SampleApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IAppDbContext
{
    DbSet<Book> Books { get; }

    DbSet<Author> Authors { get; }

    Task<IReadOnlyList<T>> QueryAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default);

    Task<T> QueryFirstOrDefaultAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default);

    Task<T> QuerySingleAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
