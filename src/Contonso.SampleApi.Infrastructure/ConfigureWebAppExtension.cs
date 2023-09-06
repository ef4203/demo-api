namespace Contonso.SampleApi.Infrastructure;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Infrastructure.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

    private static WebApplication UseAutomaticMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }

        return app;
    }

    private static WebApplication ScheduleJobs(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var backgroundJobService = scope.ServiceProvider.GetRequiredService<IJobClient>();

        var jobs = typeof(IJob).Assembly
            .GetTypes()
            .Where(x => x.IsClass && x.GetInterface(nameof(IJob)) != null);

        foreach (var type in jobs)
        {
            var jobInstance = (IJob)scope.ServiceProvider.GetRequiredService(type);
            backgroundJobService.AddOrUpdate(
                type.Name,
                () => jobInstance.Handle(),
                jobInstance.CronPattern);
        }

        return app;
    }
}
