using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Basket_Module
{
   public class CustomerBasket
    {
        public string Id { get; set; }//Guid //:Created From Client [Front-End]
        public ICollection<BasketItems> Items { get; set; } = [];
        public decimal ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }

        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
