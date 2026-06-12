using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Module;
using Talabat.Core.Entities.Product_Module;

namespace Talabat.Repository.Data.Seed
{
    public static class StoreDbContextSeed
    {
        public static async Task Seed(StoreDbContext dbContext)
        {
            #region Brand
            if (!dbContext.ProductBrands.Any())
            {
                var Brands = File.ReadAllText("../Talabat.Repository/Data/Seed/Data/brands.json");
                var BrandData = JsonSerializer.Deserialize < List<ProductBrand>>(Brands);
                if (BrandData?.Count > 0)
                {
                    foreach (var Brand in BrandData)
                    {
                      await  dbContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                   await dbContext.SaveChangesAsync();
                }
             
            }

            #endregion
            #region Category
            if (!dbContext.ProductCategories.Any())
            {
                var categories = File.ReadAllText("../Talabat.Repository/Data/Seed/Data/types.json");
                var categoryData = JsonSerializer.Deserialize < List<ProductCategory>>(categories);
                if (categoryData?.Count > 0)
                {
                    foreach (var category in categoryData)
                    {
                      await  dbContext.Set<ProductCategory>().AddAsync(category);
                    }
                   await dbContext.SaveChangesAsync();
                }

            }
            #endregion
            #region Product
            if (!dbContext.Products.Any())
            {
                var products = File.ReadAllText("../Talabat.Repository/Data/Seed/Data/products.json");
                var productData = JsonSerializer.Deserialize < List<Product>>(products);
                if (productData?.Count > 0)
                {
                    foreach (var product in productData)
                    {
                       await dbContext.Set<Product>().AddAsync(product);
                    }
                   await dbContext.SaveChangesAsync();
                }

            }

            #endregion

            #region DeliveryMethods
            if (!dbContext.DeliveryMethods.Any())
            {
                var deliveries = File.ReadAllText("../Talabat.Repository/Data/Seed/Data/delivery.json");
                var deliveryData = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveries);
                if (deliveryData?.Count > 0)
                {
                    foreach (var delivery in deliveryData)
                    {
                        await dbContext.Set<DeliveryMethod>().AddAsync(delivery);
                    }
                    await dbContext.SaveChangesAsync();
                }

            }
            #endregion
        }

    }
}
