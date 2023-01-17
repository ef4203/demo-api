namespace Contonso.SampleApi.Infrastructure;

using Hangfire;
using Microsoft.AspNetCore.Builder;

public static class ConfigureWebApplicationExtension
{
    public static IApplicationBuilder UseInfrastructureServices(this IApplicationBuilder app)
    {
        _ = app ?? throw new ArgumentNullException(nameof(app));

        app.UseHangfireDashboard();

        return app;
    }
}