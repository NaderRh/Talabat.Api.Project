using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product_Module;
using Talabat.Core.Specifcation.ProductSpecs;

namespace Talabat.Core.Service.Contract

{
   public interface IProductService
    {
        Task<IReadOnlyList<Product>>GetProductsAsync(ProductSpecParams specParams);

        Task<Product> GetProductAsync(int ProductId);

        Task<int> GetCountAsync(ProductSpecParams specParams);

        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();

        Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
    }
}
