using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dto.Basket_Dto;
using Talabat.Core.Entities.Basket_Module;

namespace Talabat.Core.Repositoriy.Contract
{
   public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string key);
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan? TimeToLive=null);
        Task<bool>DeleteBasketAsync(string id);
    }
}
