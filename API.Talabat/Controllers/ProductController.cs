using Talabat.Core.Repositories.Dto;
using API.Talabat.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifcation.ProductSpecs;
using Talabat.Core.Entities;
using API.Talabat.Errors;
using Talabat.Core.Entities.Product_Module;
using Talabat.Core.Service.Contract;

namespace API.Talabat.Controllers
{
   
    public class ProductController : ApiBaseController
    {
     
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService,IMapper mapper)
        {
          
            _productService = productService;
            _mapper = mapper;
        }
       
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>>GetProducts([FromQuery]ProductSpecParams specParams)
        {
            
            var products = await _productService.GetProductsAsync(specParams);


            var count = await _productService.GetCountAsync(specParams);

            var data=_mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageSize,specParams.PageIndex,data,count));

        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>>GetProductById(int id)
        {
           
            var product = await _productService.GetProductAsync(id);
            if (product == null)
                return NotFound(new ApiResponse(404, $"Product With Id : {id} not found"));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands=await _productService.GetBrandsAsync();
            return Ok(brands);  
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories=await _productService.GetCategoriesAsync();
            return Ok(categories);
        }

    }
}
