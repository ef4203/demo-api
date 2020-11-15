namespace Contonso.Common.AspNetCore
{
    using System;
    using Contonso.Common.Domain;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     A base class for endpoint controller classes.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        ///     Creates a status code from a given <see cref="ServiceResult{TData}" />.
        /// </summary>
        /// <typeparam name="T">The entity.</typeparam>
        /// <param name="serviceResult">The service result.</param>
        /// <returns>The <see cref="ActionResult{T}" /> with the given status code.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="serviceResult" /> is null.</exception>
        /// <exception cref="NotImplementedException">Thrown when the status code is not implemented.</exception>
        protected ActionResult<T> StatusCode<T>(ServiceResult<T> serviceResult)
        {
            _ = serviceResult ?? throw new ArgumentNullException(nameof(serviceResult));

            return serviceResult.Status switch
            {
                ServiceResultStatus.NotFound => this.NotFound(),
                ServiceResultStatus.Success => this.Ok(serviceResult.Data),
                ServiceResultStatus.BadRequest => this.BadRequest(),
                ServiceResultStatus.Created => this.Created(
                    new Uri($"{this.Request?.Scheme}://{this.Request?.Host}{this.Request?.Path}"),
                    serviceResult.Data),
                ServiceResultStatus.AuthorizationError => this.Unauthorized(),
                ServiceResultStatus.ValidationError => this.UnprocessableEntity(),
                ServiceResultStatus.DuplicateError => this.Conflict(),
                _ => throw new NotImplementedException($"Status code '{serviceResult.Status}' not implemented."),
            };
        }
    }
}
