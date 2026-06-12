using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Module;

namespace Talabat.Core.Specifcation.OrderSpecs
{
   public class OrderSpecification:BaseSpecefication<Order>
    {
        public OrderSpecification(string buyerEmail):base(P=>P.BuyerEmail== buyerEmail)
        {
            AddInclude(P => P.DeliveryMethod);
            AddInclude(P => P.Items);
        }
        public OrderSpecification(string buyerEmail,int id) : base(P => P.BuyerEmail == buyerEmail&&P.Id==id)
        {
            AddInclude(P => P.DeliveryMethod);
            AddInclude(P => P.Items);
        }
    }
}
