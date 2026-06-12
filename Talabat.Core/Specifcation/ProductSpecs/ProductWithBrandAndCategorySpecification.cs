using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Product_Module;

namespace Talabat.Core.Specifcation.ProductSpecs
{
    public class ProductWithBrandAndCategorySpecification:BaseSpecefication<Product>
    {
        public ProductWithBrandAndCategorySpecification(ProductSpecParams SpecParams):base(p=>
              (string.IsNullOrEmpty(SpecParams.Search) ||p.Name.ToLower().Contains(SpecParams.Search) &&
            (!SpecParams.BrandId.HasValue || p.ProductBrandId== SpecParams.BrandId) &&
            (!SpecParams.CaregoryId.HasValue || p.ProductTypeId== SpecParams.CaregoryId)))

        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductCategory);
            switch(SpecParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                    case "priceDes":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;

            }
            ApplyPaging((SpecParams.PageIndex-1)*SpecParams.PageSize,SpecParams.PageSize);



        }
        public ProductWithBrandAndCategorySpecification(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductCategory);
        }
    }
}
