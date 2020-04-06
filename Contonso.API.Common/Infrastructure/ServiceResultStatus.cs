namespace Contonso.API.Common.Infrastructure
{
    /// <summary>
    /// Represents the enumeration of service result statuses mapped to HTTP response codes.
    /// </summary>
    public enum ServiceResultStatus
    {
        /// <summary>
        /// The 200 status code.
        /// </summary>
        Success = 200,

        /// <summary>
        /// The 201 status code.
        /// </summary>
        Created = 201,

        /// <summary>
        /// The 400 status code.
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// The 401 status code.
        /// </summary>
        AuthorizationError = 401,

        /// <summary>
        /// The 404 status code.
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// The 422 status code.
        /// </summary>
        ValidationError = 422,

        /// <summary>
        /// The 409 status code.
        /// </summary>
        DuplicateError = 409,
    }
}
