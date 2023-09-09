namespace Contonso.SampleApi.Application.Common.Exceptions;

using System.Runtime.Serialization;
using FluentValidation.Results;

[Serializable]
public class ValidationException : Exception
{
    public ValidationException(string message)
        : base(message)
    {
    }

    public ValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        this.Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        this.Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    protected ValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public IDictionary<string, string[]> Errors { get; } = null!;
}
