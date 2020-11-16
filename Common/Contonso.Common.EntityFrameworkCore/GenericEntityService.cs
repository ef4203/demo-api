namespace Contonso.Common.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contonso.Common.Domain;
    using Contonso.Common.EntityFrameworkCore.Abstraction;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Provides service methods for database operations related to any given <see cref="ApplicationEntity" /> entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the application entity.</typeparam>
    public abstract class GenericEntityService<TEntity>
        where TEntity : ApplicationEntity
    {
        /// <summary>
        ///     The application database context.
        /// </summary>
        [NotNull]
        private readonly GenericDbContext context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericEntityService{TEntity}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">Throw when the <paramref name="context" /> is null.</exception>
        public GenericEntityService(GenericDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        ///     Gets all of the <typeparamref name="TEntity" /> entities.
        /// </summary>
        /// <returns>All of the <typeparamref name="TEntity" /> entities.</returns>
        public virtual async Task<ServiceResult<IEnumerable<TEntity>>> GetAllAsync()
        {
            var isArchivable = typeof(TEntity).GetInterfaces()
                .Any(x => x == typeof(IArchivable));

            IEnumerable<TEntity> result;
            if (isArchivable)
            {
                result = await this.context.Set<TEntity>()
                    .Where(x => !((IArchivable)x).IsDeleted)
                    .ToListAsync();
            }
            else
            {
                result = await this.context.Set<TEntity>()
                    .ToListAsync();
            }

            return ServiceResult<IEnumerable<TEntity>>.Success(result);
        }

        /// <summary>
        ///     Gets the <typeparamref name="TEntity" /> by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <typeparamref name="TEntity" /> result.</returns>
        public virtual async Task<ServiceResult<TEntity>> GetByIdAsync(Guid id)
        {
            var result = await this.context.FindAsync<TEntity>(id);

            if (result is IArchivable entity && entity.IsDeleted)
            {
                return ServiceResult<TEntity>.NotFound();
            }

            return ServiceResult<TEntity>.ShouldExist(result);
        }

        /// <summary>
        ///     Creates the <typeparamref name="TEntity" /> from specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The created <typeparamref name="TEntity" /> result.</returns>
        public virtual async Task<ServiceResult<TEntity>> CreateAsync(TEntity data)
        {
            if (data == null)
            {
                return ServiceResult<TEntity>.ValidationError();
            }

            var result = await this.context.AddAsync(data);
            await this.context.SaveChangesAsync();

            return ServiceResult<TEntity>.Created(result.Entity);
        }

        /// <summary>
        ///     Updates the <typeparamref name="TEntity" /> by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns>The updated <typeparamref name="TEntity" />.</returns>
        public virtual async Task<ServiceResult<TEntity>> UpdateAsync(Guid id, TEntity data)
        {
            if (data == null)
            {
                return ServiceResult<TEntity>.ValidationError();
            }

            var target = await this.context.FindAsync<TEntity>(id);

            if (target == null)
            {
                return ServiceResult<TEntity>.NotFound();
            }

            this.context.Update(target, data);
            await this.context.SaveChangesAsync();

            return ServiceResult<TEntity>.Success(data);
        }

        /// <summary>
        ///     Deletes the <typeparamref name="TEntity" /> specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The deletion result.</returns>
        public virtual async Task<ServiceResult<bool>> DeleteAsync(Guid id)
        {
            var target = await this.context.FindAsync<TEntity>(id);

            if (target == null)
            {
                return ServiceResult<bool>.NotFound();
            }

            this.context.Remove(target);
            await this.context.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
    }
}
