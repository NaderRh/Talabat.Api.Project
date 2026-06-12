using API.Talabat.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Dto.Basket_Dto;
using Talabat.Core.Entities.Basket_Module;
using Talabat.Core.Repositoriy.Contract;
using Talabat.Core.Repository.Contract;

namespace API.Talabat.Controllers
{
    
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
           _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>CreateOrUpdateBasket(BasketDto basketDto)
        {
            var mappedBasket=_mapper.Map<BasketDto, CustomerBasket>(basketDto);
            var Result=await _basketRepository.CreateOrUpdateBasketAsync(mappedBasket);
            if (Result is null)
                return BadRequest(new ApiResponse(400));
            return Ok(Result);
        }
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>>DeleteBasket(string key)
        {
            var Basket=await _basketRepository.GetBasketAsync(key);
            if (Basket is null)
                return NotFound(new ApiResponse(404, $"Basket With Key {key} not found"));
            var Result = await _basketRepository.DeleteBasketAsync(key);
            return Ok(Result);

        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>GetBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null)
                return NotFound(new ApiResponse(404, $"Basket Wit id : {basketId} not found"));
            return Ok(basket);
        }
    }
}
