namespace Contonso.SampleApi.Infrastructure.Persistence;

using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;

public class ReadOnlyDbContext : IDisposable
{
    private readonly DbConnection connection;

    private bool isDisposed;

    public ReadOnlyDbContext(AppDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        if (!dbContext.Database.IsRelational())
        {
            throw new NotSupportedException("Cannot use SQL on non-relational databases");
        }

        this.connection = dbContext.Database.GetDbConnection();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IEnumerable<T> Query<T>(string sql)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(ReadOnlyDbContext));

        return this.connection.Query<T>(sql);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            this.connection.Dispose();
        }

        this.isDisposed = true;
    }
}
