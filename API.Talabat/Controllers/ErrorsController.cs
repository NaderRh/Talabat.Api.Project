
using API.Talabat.Controllers;
using API.Talabat.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return code switch
            {
                400 => BadRequest(new ApiResponse(400)),
                401 => Unauthorized(new ApiResponse(401)),
                403 => StatusCode(403, new ApiResponse(403)),
                404 => NotFound(new ApiResponse(404)),
                _ => StatusCode(code, new ApiResponse(code))
            };
        }
    }
}
