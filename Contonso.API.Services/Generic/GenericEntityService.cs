namespace Contonso.API.Services.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contonso.API.Data;
    using Contonso.API.Entities;
    using Contonso.API.Services.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Provides service methods for database operations related to any given <see cref="ApplicationEntity"/> entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the application entity.</typeparam>
    public abstract class GenericEntityService<TEntity>
            where TEntity : ApplicationEntity
    {
        /// <summary>
        /// The application database context.
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEntityService{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">Throw when the <paramref name="context"/> is null.</exception>
        public GenericEntityService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets all of the <typeparamref name="TEntity"/> entities.
        /// </summary>
        /// <returns>All of the <typeparamref name="TEntity"/> entities.</returns>
        public virtual async Task<ServiceResult<IEnumerable<TEntity>>> GetAll()
        {
            var result = await this.context.Set<TEntity>().ToListAsync();

            return ServiceResult<IEnumerable<TEntity>>.Success(result);
        }

        /// <summary>
        /// Gets the <typeparamref name="TEntity"/> by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <typeparamref name="TEntity"/> result.</returns>
        public virtual async Task<ServiceResult<TEntity>> Get(Guid id)
        {
            var result = await this.context.FindAsync<TEntity>(id);

            return ServiceResult<TEntity>.ShouldExist(result);
        }

        /// <summary>
        /// Creates the <typeparamref name="TEntity"/> from specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The created <typeparamref name="TEntity"/> result.</returns>
        public virtual async Task<ServiceResult<TEntity>> Create(TEntity data)
        {
            var result = await this.context.AddAsync<TEntity>(data);

            await this.context.SaveChangesAsync();

            return ServiceResult<TEntity>.Created(result.Entity);
        }

        /// <summary>
        /// Updates the <typeparamref name="TEntity"/> by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns>The updated <typeparamref name="TEntity"/>.</returns>
        public virtual async Task<ServiceResult<TEntity>> Update(Guid id, TEntity data)
        {
            var target = await this.Get(id);

            if (target.Status == ServiceResultStatus.NotFound)
            {
                return ServiceResult<TEntity>.NotFound();
            }

            data.Id = target.Data.Id;

            var result = this.context.Update<TEntity>(data);

            await this.context.SaveChangesAsync();

            return ServiceResult<TEntity>.Success(result.Entity);
        }

        /// <summary>
        /// Deletes the <typeparamref name="TEntity"/> specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The deletion result.</returns>
        public virtual async Task<ServiceResult<bool>> Delete(Guid id)
        {
            var target = await this.Get(id);

            if (target.Status == ServiceResultStatus.NotFound)
            {
                return ServiceResult<bool>.NotFound();
            }

            this.context.Remove<TEntity>(target.Data);

            await this.context.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
    }
}
