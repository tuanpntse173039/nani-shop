using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extenstions
{
    public static class SeedDataServiceExtensions
    {
        public static async void AddSeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = serviceProvider.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred during migration");
                }
            }
        }
    }
}

/*

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = serviceProvider.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration");
    }
}
*/
