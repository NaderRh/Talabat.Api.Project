using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Module;

namespace Talabat.Core.Dto.Order_Dto
{
   public class OrderToReturnDto
    {
        public  int Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }

        public decimal DeliverMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }=new HashSet<OrderItemDto>();

        public decimal SupTotal { get; set; }

        public decimal Total { get; set; }
    }
}
