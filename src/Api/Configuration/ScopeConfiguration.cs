﻿using Infrastructure.Context;

namespace Api.Configuration
{
    public static class ScopeConfiguration
    {
        public static Task ConfigureScopeAsync(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<OracleContext>();
                // context.Database.Migrate(); // Apply migrations
                //SeedData.Initialize(context); // Add data for testing
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
            return Task.CompletedTask;
        }
    }
}
