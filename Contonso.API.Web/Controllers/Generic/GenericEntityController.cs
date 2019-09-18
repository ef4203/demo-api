namespace Contonso.API.Web.Controllers.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contonso.API.Entities;
    using Contonso.API.Services.Generic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Provides HTTP endpoints for any given <see cref="ApplicationEntity"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Controller" />
    public abstract class GenericEntityController<TService, TEntity> : BaseController
        where TEntity : ApplicationEntity
        where TService : GenericEntityService<TEntity>
    {
        /// <summary>
        /// The service for the given <typeparamref name="TEntity"/>.
        /// </summary>
        private readonly TService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEntityController{TService, TEntity}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="service"/> is null.</exception>
        public GenericEntityController(TService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Gets all the <typeparamref name="TEntity"/> entities.
        /// </summary>
        /// <returns>All the <typeparamref name="TEntity"/> entities.</returns>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var result = await this.service.GetAll();

            return this.StatusCode(result);
        }

        /// <summary>
        /// Gets the <typeparamref name="TEntity"/> by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <typeparamref name="TEntity"/>.</returns>
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById([FromRoute]Guid id)
        {
            var result = await this.service.Get(id);

            return this.StatusCode(result);
        }

        /// <summary>
        /// Creates the <typeparamref name="TEntity"/> from specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The created <typeparamref name="TEntity"/>.</returns>
        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Create([FromBody]TEntity data)
        {
            var result = await this.service.Create(data);

            return this.StatusCode(result);
        }

        /// <summary>
        /// Updates the <typeparamref name="TEntity"/> by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The updated data.</param>
        /// <returns>The updated <typeparamref name="TEntity"/>.</returns>
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TEntity>> Update([FromRoute]Guid id, [FromBody]TEntity data)
        {
            var result = await this.service.Update(id, data);

            return this.StatusCode(result);
        }

        /// <summary>
        /// Deletes the <typeparamref name="TEntity"/> specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The deletion result.</returns>
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<bool>> Delete([FromRoute]Guid id)
        {
            var result = await this.service.Delete(id);

            return this.StatusCode(result);
        }
    }
}