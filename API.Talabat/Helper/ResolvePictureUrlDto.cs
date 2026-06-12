using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Product_Module;
using Talabat.Core.Repositories.Dto;

namespace API.Talabat.Helper
{
    public class ResolvePictureUrlDto : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ResolvePictureUrlDto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            return string.Empty;
        }
    }
}
