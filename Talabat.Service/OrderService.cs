using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Module;
using Talabat.Core.Entities.Product_Module;
using Talabat.Core.Repositoriy.Contract;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifcation.OrderSpecs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork,IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
           _paymentService = paymentService;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
          var deliveryMethods =await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            var orderItems=new List<OrderItem>();   
            if(basket?.Items?.Count() > 0)
            {
                var productRepo =  _unitOfWork.Repository<Product>();
                foreach(var item in basket.Items)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    if (product is null)
                        throw new Exception($"Product with id {item.Id} not found");
                    var productItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            var subTotal=orderItems.Sum(P=>P.Price*P.Quantity);
            var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId); 

            var orderSpec = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);
            var orderRepo =  _unitOfWork.Repository<Order>(); 
            var existingOrder=await orderRepo.GetByIdWithSpecAsync(orderSpec);


            if (existingOrder != null)
            {
                orderRepo.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }
            var order=new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal,basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            var result=await _unitOfWork.CompleteAsync();
            if (result <= 0)
                throw new Exception("Failed to create order");
            return order;

           
        }

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var Spec = new OrderSpecification(buyerEmail, orderId);
            var order=await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(Spec);
            if (order == null)
                return null;
            return order;

        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var Spec=new OrderSpecification(buyerEmail);
            var Order=await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);
            return Order;
        }
    }
}
