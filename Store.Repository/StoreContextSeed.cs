using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Store.Repository/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands != null)
                        await context.ProductBrands.AddRangeAsync(brands);
                }

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Store.Repository/SeedData/types.json");

                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    if (Types != null)
                        await context.ProductTypes.AddRangeAsync(Types);
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Store.Repository/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products != null)
                        await context.Products.AddRangeAsync(products);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreDbContext>();
                logger.LogError(ex.Message);
            }

        }
    }
}
