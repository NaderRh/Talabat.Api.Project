using API.Talabat.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.Xml;
using Talabat.Core.Entities.Basket_Module;
using Talabat.Core.Entities.Order_Module;
using Talabat.Core.Service.Contract;

namespace API.Talabat.Controllers
{
   
    public class PaymentController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
       
        
        public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger,IConfiguration configuration)
        {
            _paymentService = paymentService;
            _logger = logger;
           _configuration = configuration;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>>CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket=await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null)
                return BadRequest(new ApiResponse(400, "an Error in Your Basket"));
            return Ok(basket);
        }
        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, signature, _configuration["StripeSettings:WhSecret"]);

                 
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded || stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                     
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    if (paymentIntent != null)
                    {
                        bool isSucceeded = (stripeEvent.Type == EventTypes.PaymentIntentSucceeded);

                       
                        await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, isSucceeded);

                        _logger.LogInformation($"PaymentIntent Successfully Updated: {paymentIntent.Id}");
                    }
                }
                else
                {
                    _logger.LogInformation($"Error Event Type {stripeEvent.Type}");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Through processing {ex.Message}");
                return BadRequest();
            }
        }
        [HttpPost("simulate-success/{paymentIntentId}")]
        public async Task<IActionResult>SimulateSuccess(string paymentIntentId)
        {
            var order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntentId, true);
            return Ok(order);
        }
        [HttpPost("simulate-failed/{paymentIntentId}")]
        public async Task<IActionResult> SimulateFailed(string paymentIntentId)
        {
            var order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntentId, false);
            return Ok(order);
        }
        [HttpPost("confirm/{paymentIntentId}")]
        public async Task<IActionResult> ConfirmPaymentIntent(string paymentIntentId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var service = new PaymentIntentService();

            var result = await service.ConfirmAsync(
                paymentIntentId,
                new PaymentIntentConfirmOptions
                {
                    PaymentMethod = "pm_card_visa"
                });

            return Ok(result);
        }

    }

    }

