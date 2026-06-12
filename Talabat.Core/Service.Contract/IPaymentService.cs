using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Basket_Module;
using Talabat.Core.Entities.Order_Module;

namespace Talabat.Core.Service.Contract
{
   public interface IPaymentService
    {
        Task<CustomerBasket>CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId,bool isSucceded);
    }
}
