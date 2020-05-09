namespace Contonso.API.Common.Extensions
{
    using System;
    using System.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Provides extension methods for the <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="app">The application.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="app"/> is null.</exception>
        /// <exception cref="StrongTypingException">Throw when <typeparamref name="TContext"/> is of wrong type.</exception>
        public static void InitializeDatabase<TContext>(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<TContext>() as DbContext;

            if (dbContext == null)
            {
                throw new StrongTypingException(typeof(TContext).Name);
            }

            dbContext.Database.Migrate();
        }
    }
}