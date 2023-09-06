namespace Contonso.SampleApi.Application.Common.Abstraction;

public interface IJob
{
    string CronPattern { get; }

    Task Handle();
}
