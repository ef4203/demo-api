namespace Contonso.SampleApi.Web;

using Contonso.SampleApi.Application;
using Contonso.SampleApi.Infrastructure;
using Contonso.SampleApi.Web.Filter;
using Microsoft.AspNetCore.OData;
using Prometheus;
using Serilog;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging.
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        builder.Services.AddControllersWithViews(
                o =>
                    o.Filters.Add<ApiExceptionFilterAttribute>())
            .AddNewtonsoftJson()
            .AddOData(o => { o.EnableQueryFeatures(1000); });

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseInfrastructureServices();
        app.UseHealthChecks("/health");
        app.UseMetricServer();
        app.UseHttpMetrics();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
