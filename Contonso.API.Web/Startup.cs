namespace Contonso.API.Web
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.Json.Serialization;
    using Contonso.API.Data;
    using Contonso.API.Domain;
    using Contonso.Common.AspNetCore.Extensions;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Serilog;

    /// <summary>
    ///     Contains Configuration which will be run on startup.
    /// </summary>
    public sealed class Startup
    {
        private readonly string applicationName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.applicationName = Assembly.GetEntryAssembly()?.GetName().Name;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)?
                .CreateLogger();
        }

        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <value>
        ///     The configuration.
        /// </value>
        [NotNull]
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));

            var dbConnectionString = this.Configuration.GetConnectionString("Database") ?? string.Empty;

            services.AddDbContextPool<ApplicationDbContext>(
                o =>
                    o!.UseSqlServer(dbConnectionString));

            services.AddTransient<BookService>();

            services.AddSwaggerGen(
                o =>
                {
                    o.SwaggerDoc("v1", new OpenApiInfo { Title = this.applicationName, Version = "v1" });
                    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{this.applicationName}.xml"));
                });

            services.AddControllers()
                .AddJsonOptions(o => { o?.JsonSerializerOptions?.Converters.Add(new JsonStringEnumConverter()); });
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app ?? throw new ArgumentNullException(nameof(app));
            _ = env ?? throw new ArgumentNullException(nameof(env));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    o =>
                    {
                        o!.SwaggerEndpoint("/swagger/v1/swagger.json", this.applicationName);
                        o!.RoutePrefix = string.Empty;
                    });
            }

            app.InitializeDatabase<ApplicationDbContext>();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
