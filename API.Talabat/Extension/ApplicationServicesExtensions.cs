using API.Talabat.Errors;
using API.Talabat.Helper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositoriy.Contract;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Repository.Repository;
using Talabat.Service;

namespace API.Talabat.Extension
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddAutoMapper(typeof(MappingProfile));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddScoped(typeof(IProductService), typeof(ProductService));
            Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));

            #region ValidationError
            Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (actionDbcontext) =>
                {
                    var errors = actionDbcontext.ModelState.Where(e => e.Value.Errors.Count() > 0)
                                                         .SelectMany(e => e.Value.Errors)
                                                         .Select(e => e.ErrorMessage).ToList();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors

                    };
                    return new BadRequestObjectResult(response);
                };

            });
            #endregion

            return Services;
        }
    }
}
