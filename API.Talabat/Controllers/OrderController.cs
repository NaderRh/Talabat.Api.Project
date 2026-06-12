using API.Talabat.Errors;
using API.Talabat.Extension;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Dto.Order_Dto;
using Talabat.Core.Entities.Order_Module;
using Talabat.Core.Service.Contract;

namespace API.Talabat.Controllers
{
    [Authorize]
    public class OrderController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper,IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderToReturnDto>>CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.GetUserEmail();
            var Address=_mapper.Map<AddressDto,Address>(orderDto.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, Address);

            if (order is null)
                return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order,OrderToReturnDto>(order));
        }
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>>GetOrdersFoUser()
        {
            var buyerEmail = User.GetUserEmail();
            var order=await _orderService.GetOrderForUserAsync(buyerEmail);
            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(order));
              
        }
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderForUser(int orderId)
        {
            var buyerEmail = User.GetUserEmail();
            var Order =await _orderService.GetOrderByIdForUserAsync(orderId, buyerEmail);
            if (Order is null) return NotFound(new ApiResponse(404));
            return Ok(Order);
        }
        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }

    }
}
