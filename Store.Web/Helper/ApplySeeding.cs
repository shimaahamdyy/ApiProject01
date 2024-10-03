using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Repository;

namespace Store.Web.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var Services = scope.ServiceProvider;

                var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var Context = Services.GetRequiredService<StoreDbContext>();

                    await StoreContextSeed.SeedAsync(Context, loggerFactory);  

                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }

        }
    }
}
