namespace Contonso.API.Common.Web
{
    using System;
    using Contonso.API.Common.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A base class for endpoint controller classes.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Creates a status code from a given <see cref="ServiceResult{TData}"/>.
        /// </summary>
        /// <typeparam name="T">The entity.</typeparam>
        /// <param name="serviceResult">The service result.</param>
        /// <returns>The <see cref="ActionResult{T}"/> with the given status code.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="serviceResult"/> is null.</exception>
        /// <exception cref="NotImplementedException">Thrown when the status code is not implemented.</exception>
        protected ActionResult<T> StatusCode<T>(ServiceResult<T> serviceResult)
        {
            if (serviceResult == null)
            {
                throw new ArgumentNullException(nameof(serviceResult));
            }

            switch (serviceResult.Status)
            {
                case ServiceResultStatus.NotFound:
                    return this.NotFound();

                case ServiceResultStatus.Success:
                    return this.Ok(serviceResult.Data);

                case ServiceResultStatus.BadRequest:
                    return this.BadRequest();

                case ServiceResultStatus.Created:
                    return this.Created(new Uri($"{this.Request.Scheme}://{this.Request.Host}{this.Request.Path}"), serviceResult.Data);

                case ServiceResultStatus.AuthorizationError:
                    return this.Unauthorized();

                case ServiceResultStatus.ValidationError:
                    return this.UnprocessableEntity();

                case ServiceResultStatus.DuplicateError:
                    return this.Conflict();

                default:
                    throw new NotImplementedException(Properties.Resources.StatusCodeNotImplemented);
            }
        }
    }
}