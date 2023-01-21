namespace Contonso.SampleApi.Infrastructure.Scheduling;

using System.Linq.Expressions;
using Hangfire;

public class ApplicationBackgroundJobService : IApplicationBackgroundJobService
{
    private readonly IBackgroundJobClient backgroundJobClient;

    private readonly IRecurringJobManager recurringJobManager;

    public ApplicationBackgroundJobService(
        IBackgroundJobClient backgroundJobClient,
        IRecurringJobManager recurringJobManager)
    {
        this.backgroundJobClient =
            backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
        this.recurringJobManager = recurringJobManager ?? throw new ArgumentNullException(nameof(recurringJobManager));
    }

    public string Enqueue(Expression<Action> methodCall)
    {
        return this.backgroundJobClient.Enqueue(methodCall);
    }

    public string Enqueue(Expression<Func<Task>> methodCall)
    {
        return this.backgroundJobClient.Enqueue(methodCall);
    }

    public string Schedule(Expression<Action> methodCall, TimeSpan delay)
    {
        return this.backgroundJobClient.Schedule(methodCall, delay);
    }

    public string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay)
    {
        return this.backgroundJobClient.Schedule(methodCall, delay);
    }

    public void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, string cronPattern)
    {
        this.recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronPattern);
    }

    public void AddOrUpdate(string recurringJobId, Expression<Func<Task>> methodCall, string cronPattern)
    {
        this.recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronPattern);
    }

    public string ContinueJobWith(string parentJobId, Expression<Action> methodCall)
    {
        return this.backgroundJobClient.ContinueJobWith(parentJobId, methodCall);
    }

    public string ContinueJobWith(string parentJobId, Expression<Func<Task>> methodCall)
    {
        return this.backgroundJobClient.ContinueJobWith(parentJobId, methodCall);
    }
}
