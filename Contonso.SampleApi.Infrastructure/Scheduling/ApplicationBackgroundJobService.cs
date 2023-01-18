using System.Linq.Expressions;

namespace Contonso.SampleApi.Infrastructure.Scheduling;

using Hangfire;

public class ApplicationBackgroundJobService : IApplicationBackgroundJobService
{
    private readonly IBackgroundJobClient backgroundJobClient;

    private readonly IRecurringJobManager recurringJobManager;
    
    public ApplicationBackgroundJobService(IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
    {
        this.backgroundJobClient =
            backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
        this.recurringJobManager = recurringJobManager ?? throw new ArgumentNullException(nameof(recurringJobManager));
    }
    
    public string Enqueue(Expression<Action> methodCall)
        => this.backgroundJobClient.Enqueue(methodCall);

    public string Enqueue(Expression<Func<Task>> methodCall)
        => this.backgroundJobClient.Enqueue(methodCall);

    public string Schedule(Func<Action> methodCall, TimeSpan delay)
        => this.backgroundJobClient.Schedule(() => methodCall(), delay);

    public string Schedule(Func<Task> methodCall, TimeSpan delay)
        => this.backgroundJobClient.Schedule(() => methodCall(), delay);

    public void AddOrUpdate(string jobName, Func<Action> methodCall, string cronPattern)
        => this.recurringJobManager.AddOrUpdate(jobName, () => methodCall(), cronPattern);

    public void AddOrUpdate(string jobName, Func<Task> methodCall, string cronPattern)
        => this.recurringJobManager.AddOrUpdate(jobName, () => methodCall(), cronPattern);

    public string ContinueJobWith(string parentJobId, Func<Action> methodCall)
        => this.backgroundJobClient.ContinueJobWith(parentJobId, () => methodCall());

    public string ContinueJobWith(string parentJobId, Func<Task> methodCall)
        => this.backgroundJobClient.ContinueJobWith(parentJobId, () => methodCall());
}