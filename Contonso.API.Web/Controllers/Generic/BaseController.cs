namespace Contonso.API.Web.Controllers
{
    using System;
    using Contonso.API.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The base controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseController : Controller
    {
        /// <summary>
        /// Statuses the code.
        /// </summary>
        /// <typeparam name="T">The entity.</typeparam>
        /// <param name="serviceResult">The service result.</param>
        /// <returns>The status code.</returns>
        /// <exception cref="System.NotImplementedException">Status code not implemented.</exception>
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