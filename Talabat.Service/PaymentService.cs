using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Basket_Module;
using Talabat.Core.Entities.Order_Module;
using Talabat.Core.Entities.Product_Module;
using Talabat.Core.Repositoriy.Contract;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifcation.OrderSpecs;
using Product = Talabat.Core.Entities.Product_Module.Product;
using stripeProduct = Stripe.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger _logger;

        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork,IBasketRepository basketRepository,ILogger<PaymentService> logger)
        {
            _configuration = configuration;
           _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _logger = logger;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            var basket=await _basketRepository.GetBasketAsync(basketId);
            if (basket is null)
                return null;
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                if(deliveryMethod is null)
                    throw new Exception($"Delivery Method With id {basket.DeliveryMethodId.Value} not found");
                shippingPrice= deliveryMethod.Cost;
                basket.ShippingPrice = shippingPrice;
            }
            if (basket?.Items.Count > 0)
            {
                var productRepo =  _unitOfWork.Repository<Product>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    if(item.Price!=product.Price)
                        item.Price= product.Price;

                }
            }
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(P => P.Price * 100 * P.Quantity) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                };
                paymentIntent=await paymentIntentService.CreateAsync(option);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(P => P.Price * 100 * P.Quantity) + (long)shippingPrice * 100
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }
            await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceded)
        {
            var spec = new OrderWithPaymentIntentSpecifications(paymentIntentId);
            var order=await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if(order is null)
            {
                _logger.LogError($"Order not Found For PaymentIntent {paymentIntentId}");
                return null;
            }
            if (isSucceded)
                order.Status=OrderStatus.PaymentSucceeded;
            else
                order.Status = OrderStatus.PaymentFailed;
             _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.CompleteAsync();
            return order;
        }
    }
}
