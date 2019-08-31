namespace Contonso.API.Web.Controllers.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contonso.API.Entities;
    using Contonso.API.Services.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A generic controller that provides enpoints to entities.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Contonso.API.Web.Controllers.BaseController" />
    public class GenericController<TService, TEntity> : BaseController
        where TEntity : ApplicationEntity
        where TService : GenericEntityService<TEntity>
    {
        private readonly TService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericController{TService, TEntity}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <exception cref="ArgumentNullException">service.</exception>
        public GenericController(TService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>All entities.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var result = await this.service.GetAll();

            return this.StatusCode(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TEntity>> GetById([FromRoute]Guid id)
        {
            var result = await this.service.Get(id);

            return this.StatusCode(result);
        }

        /// <summary>
        /// Creates the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The creation result.</returns>
        [HttpPost]
        public async Task<ActionResult<TEntity>> Create([FromBody]TEntity data)
        {
            var result = await this.service.Create(data);

            return this.StatusCode(result);
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns>The update result.</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<TEntity>> Update([FromRoute]Guid id, [FromBody]TEntity data)
        {
            var result = await this.service.Update(id, data);

            return this.StatusCode(result);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The delete result.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute]Guid id)
        {
            var result = await this.service.Delete(id);

            return this.StatusCode(result);
        }
    }
}