using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Dto
{
    public class ProductToReturnDto
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductBrandId { get; set; }
        public string Brand { get; set; }
        public int ProductTypeId { get; set; }
        public string Category { get; set; }
    }
}
