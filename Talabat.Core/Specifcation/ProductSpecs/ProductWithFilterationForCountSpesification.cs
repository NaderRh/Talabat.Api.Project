using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Product_Module;

namespace Talabat.Core.Specifcation.ProductSpecs
{
    public class ProductWithFilterationForCountSpesification:BaseSpecefication<Product>
    {
        public ProductWithFilterationForCountSpesification(ProductSpecParams SpecParams):base
              (p => 
              (string.IsNullOrEmpty(SpecParams.Search)||p.Name.ToLower().Contains(SpecParams.Search))&&
              (!SpecParams.BrandId.HasValue || p.ProductBrandId == SpecParams.BrandId) 
              && (!SpecParams.CaregoryId.HasValue || p.ProductTypeId == SpecParams.CaregoryId))
        {
            
        }
    }
}
