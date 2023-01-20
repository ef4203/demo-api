namespace Contonso.SampleApi.Application.Common.LoggerMessages;

using Microsoft.Extensions.Logging;

public static class LoggingBehaviorMessages
{
    private static readonly Action<ILogger, string, string, Exception> BeginMsg =
        LoggerMessage.Define<string, string>(
            LogLevel.Debug,
            new EventId(1, null),
            "Start of {RequestName}, Request: {RequestObject}");

    private static readonly Action<ILogger, string, string, Exception> EndMsg =
        LoggerMessage.Define<string, string>(
            LogLevel.Debug,
            new EventId(1, null),
            "End of {RequestName}, Response: {RequestObject}");

    public static void LogRequestStart(this ILogger logger, string requestName, string requestObject)
    {
        BeginMsg(logger, requestName, requestObject, null!);
    }

    public static void LogRequestEnd(this ILogger logger, string requestName, string requestObject)
    {
        EndMsg(logger, requestName, requestObject, null!);
    }
}