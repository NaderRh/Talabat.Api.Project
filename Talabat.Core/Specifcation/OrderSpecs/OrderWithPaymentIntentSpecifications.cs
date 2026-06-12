using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Module;

namespace Talabat.Core.Specifcation.OrderSpecs
{
   public class OrderWithPaymentIntentSpecifications:BaseSpecefication<Order>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId):base(P=>P.PaymentIntentId==paymentIntentId)
        {
            AddInclude(P => P.Items);
            AddInclude(P=>P.DeliveryMethod);
        }
    }
}
