namespace Contonso.API.Web.Controllers.Generic
{
    using System;
    using Contonso.API.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A base class for endpoint controller classes.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Creates a statuscode from a given <see cref="ServiceResult{TData}"/>.
        /// </summary>
        /// <typeparam name="T">The entity.</typeparam>
        /// <param name="serviceResult">The service result.</param>
        /// <returns>The <see cref="ActionResult{T}"/> with the given status code.</returns>
        /// <exception cref="NotImplementedException">Thrown when the status code is not implemented.</exception>
        protected ActionResult<T> StatusCode<T>(ServiceResult<T> serviceResult)
        {
            switch (serviceResult.Status)
            {
                case ServiceResultStatus.NotFound:
                    return this.NotFound();

                case ServiceResultStatus.Success:
                    return this.Ok(serviceResult.Data);

                case ServiceResultStatus.BadRequest:
                    return this.BadRequest();

                case ServiceResultStatus.Created:
                    return this.Created(this.Request.Path, serviceResult.Data);

                default:
                    throw new NotImplementedException("Status code not implemented.");
            }
        }
    }
}