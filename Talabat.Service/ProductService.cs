using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product_Module;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifcation.ProductSpecs;

namespace Talabat.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> GetProductAsync(int ProductId)
        {
            var productSpec = new ProductWithBrandAndCategorySpecification(ProductId);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(productSpec);
            return product;

        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
        {
            var productSpec = new ProductWithBrandAndCategorySpecification(specParams);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(productSpec);
            return products;
        }


        public async Task<int> GetCountAsync(ProductSpecParams specParams)
        {
            var productSpec = new ProductWithBrandAndCategorySpecification(specParams);
            var productCount = await _unitOfWork.Repository<Product>().CountAsync(productSpec);
            return productCount;
        }

       

        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
      => await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
       => await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
    }
}
