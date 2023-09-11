namespace Contonso.SampleApi.Application.Abstraction;

public interface IJob
{
    string CronPattern { get; }

    Task HandleAsync();
}
