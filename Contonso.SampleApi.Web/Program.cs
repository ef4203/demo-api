namespace Contonso.SampleApi.Web;

using Contonso.SampleApi.Application;
using Contonso.SampleApi.Infrastructure;
using Contonso.SampleApi.Web.Filter;
using Microsoft.AspNetCore.OData;
using Serilog;

internal class Programm
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging.
        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();

        builder.Host.UseSerilog();

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        builder.Services.AddControllersWithViews(options => options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddNewtonsoftJson().AddOData(o => { o.EnableQueryFeatures(1000); });

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
