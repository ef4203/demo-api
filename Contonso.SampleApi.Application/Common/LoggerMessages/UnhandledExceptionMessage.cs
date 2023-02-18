namespace Contonso.SampleApi.Application.Common.LoggerMessages;

using Microsoft.Extensions.Logging;

internal static class UnhandledExceptionMessage
{
    private static readonly Action<ILogger, string, Exception> Message =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1),
            "Unhandled exception in {RequestName}");

    public static void LogUnhandledException(
        this ILogger logger,
        Exception exception,
        string requestName)
    {
        Message(logger, requestName, exception);
    }
}
