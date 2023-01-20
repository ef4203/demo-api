namespace Contonso.SampleApi.Application.Common.Abstraction;

using System.Linq.Expressions;

public interface IApplicationBackgroundJobService
{
   string Enqueue(Expression<Action> methodCall);

   string Enqueue(Expression<Func<Task>> methodCall);

   string Schedule(Expression<Action> methodCall, TimeSpan delay);

   string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay);

   void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, string cronPattern);

   void AddOrUpdate(string recurringJobId, Expression<Func<Task>> methodCall, string cronPattern);

   string ContinueJobWith(string parentJobId, Expression<Action> methodCall);

   string ContinueJobWith(string parentJobId, Expression<Func<Task>> methodCall);
}