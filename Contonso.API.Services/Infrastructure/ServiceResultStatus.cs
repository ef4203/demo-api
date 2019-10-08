namespace Contonso.API.Services.Infrastructure
{
    /// <summary>
    /// Represents the enumeration of service result statuses mapped to HTTP response codes.
    /// </summary>
    public enum ServiceResultStatus
    {
#pragma warning disable CS1584 // Warning is overriding an error
#pragma warning disable CS1658 // Warning is overriding an error
        /// <summary>
        /// The <see cref="200"/> status code.
        /// </summary>
        Success = 200,

        /// <summary>
        /// The <see cref="201"/> status code.
        /// </summary>
        Created = 201,

        /// <summary>
        /// The <see cref="400"/> status code.
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// The <see cref="401"/> status code.
        /// </summary>
        AuthorizationError = 401,

        /// <summary>
        /// The <see cref="404"/> status code.
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// The <see cref="422"/> status code.
        /// </summary>
        ValidationError = 422,

        /// <summary>
        /// The <see cref="409"/> status code.
        /// </summary>
        DuplicateError = 409,
#pragma warning restore CS1584 // Warning is overriding an error
#pragma warning restore CS1658 // Warning is overriding an error
    }
}
