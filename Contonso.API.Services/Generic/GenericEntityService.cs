namespace Contonso.API.Services.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contonso.API.Data;
    using Contonso.API.Entities;
    using Contonso.API.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// A generic service for entity actions on the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class GenericEntityService<TEntity>
            where TEntity : ApplicationEntity
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEntityService{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context.</exception>
        public GenericEntityService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>All entities.</returns>
        public async Task<ServiceResult<IEnumerable<TEntity>>> GetAll()
        {
            var result = await this.context.Set<TEntity>().ToListAsync();

            return ServiceResult<IEnumerable<TEntity>>.Success(result);
        }

        /// <summary>
        /// Gets the entity by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        public async Task<ServiceResult<TEntity>> Get(Guid id)
        {
            var result = await this.context.FindAsync<TEntity>(id);

            return ServiceResult<TEntity>.ShouldExist(result);
        }

        /// <summary>
        /// Creates the entity from specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The created entity.</returns>
        public async Task<ServiceResult<TEntity>> Create(TEntity data)
        {
            var result = await this.context.AddAsync<TEntity>(data);

            await this.context.SaveChangesAsync();

            return ServiceResult<TEntity>.Created(result.Entity);
        }

        /// <summary>
        /// Updates the entity by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns>The updated entity.</returns>
        public async Task<ServiceResult<TEntity>> Update(Guid id, TEntity data)
        {
            var target = await this.Get(id);

            if (target.Status == ServiceResultStatus.NotFound)
            {
                return ServiceResult<TEntity>.NotFound();
            }

            data.ID = target.Data.ID;

            var result = this.context.Update<TEntity>(data);

            await this.context.SaveChangesAsync();

            return ServiceResult<TEntity>.Success(result.Entity);
        }

        /// <summary>
        /// Deletes the entity specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The deletion result.</returns>
        public async Task<ServiceResult<bool>> Delete(Guid id)
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
