using System.Linq.Expressions;

namespace Contonso.SampleApi.Application.Common.Abstraction;

public interface IApplicationBackgroundJobService
{
   string Enqueue(Expression<Action> methodCall);

   string Enqueue(Expression<Func<Task>> methodCall);

   string Schedule(Func<Action> methodCall, TimeSpan delay);

   string Schedule(Func<Task> methodCall, TimeSpan delay);

   void AddOrUpdate(string jobName, Func<Action> methodCall, string ctronPattern);

   void AddOrUpdate(string jobName, Func<Task> methodCall, string cronPattern);

   string ContinueJobWith(string parentJobId, Func<Action> methodCall);

   string ContinueJobWith(string parentJobId, Func<Task> methodCall);
}