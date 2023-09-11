namespace Contonso.SampleApi.Infrastructure;

using Contonso.SampleApi.Application.Abstraction;
using Contonso.SampleApi.Infrastructure.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class ConfigureWebAppExtension
{
    public static WebApplication UseInfrastructureServices(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseHangfireDashboard();
        app.UseAutomaticMigration();
        app.ScheduleJobs();

        return app;
    }

    private static void UseAutomaticMigration(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }
    }

    private static void ScheduleJobs(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var backgroundJobService = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

        var jobs = typeof(IJob).Assembly
            .GetTypes()
            .Where(x => x.IsClass && x.GetInterface(nameof(IJob)) != null);

        foreach (var type in jobs)
        {
            var jobInstance = (IJob)scope.ServiceProvider.GetRequiredService(type);
            backgroundJobService.AddOrUpdate(
                type.Name,
                () => jobInstance.HandleAsync(),
                jobInstance.CronPattern);
        }
    }
}
