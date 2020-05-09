namespace Contonso.API.Common.Infrastructure
{
    /// <summary>
    /// Represents the service result object.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    public class ServiceResult<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResult{TData}"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        private ServiceResult(ServiceResultStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResult{TData}"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="data">The data.</param>
        private ServiceResult(ServiceResultStatus status, TData data)
            : this(status)
        {
            this.Data = data;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ServiceResultStatus Status { get; set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public TData Data { get; }

        /// <summary>
        /// The data operation was successful.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The service result.</returns>
        public static ServiceResult<TData> Success(TData data)
        {
            return new ServiceResult<TData>(ServiceResultStatus.Success, data);
        }

        /// <summary>
        /// The data was created successfully.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The service result.</returns>
        public static ServiceResult<TData> Created(TData data)
        {
            return new ServiceResult<TData>(ServiceResultStatus.Created, data);
        }

        /// <summary>
        /// The result yielded an error in validation.
        /// </summary>
        /// <returns>The service result.</returns>
        public static ServiceResult<TData> ValidationError()
        {
            return new ServiceResult<TData>(ServiceResultStatus.BadRequest);
        }

        /// <summary>
        /// The data was not found.
        /// </summary>
        /// <returns>The service result.</returns>
        public static ServiceResult<TData> NotFound()
        {
            return new ServiceResult<TData>(ServiceResultStatus.NotFound);
        }

        /// <summary>
        /// The data should exist.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The service result.</returns>
        public static ServiceResult<TData> ShouldExist(TData data)
        {
            return data != null ? Success(data) : NotFound();
        }
    }
}
