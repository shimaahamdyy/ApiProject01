using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities.IdentityEntities;
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
                    var userManager = Services.GetRequiredService<UserManager<AppUser>>();

                    //await Context.Database.MigrateAsync(); //To Create Database if it does not exist

                    await StoreContextSeed.SeedAsync(Context, loggerFactory);
                    await StoreIdentityContextSeed.SeedUserAsync(userManager);

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
