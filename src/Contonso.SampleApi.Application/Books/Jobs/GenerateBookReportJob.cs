namespace Contonso.SampleApi.Application.Books.Jobs;

using System.Text.Json;
using Contonso.SampleApi.Application.Books.Queries.GetBooks;
using Contonso.SampleApi.Application.Common.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

public class GenerateBookReportJob : IJob
{
    private readonly ILogger<GenerateBookReportJob> logger;

    private readonly ISender mediator;

    public GenerateBookReportJob(ILogger<GenerateBookReportJob> logger, ISender mediator)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public string CronPattern { get; } = "0/5 * * * *";

    public async Task Handle()
    {
        var result = await this.mediator.Send(new GetBooksQuery());
        var resultString = JsonSerializer.Serialize(result);
        this.logger.LogInformation("REPORT: {Result}", resultString);
    }
}
