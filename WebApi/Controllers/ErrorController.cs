using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi.Controllers
{
    [Route("error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseController
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public IActionResult HandleError()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            return Problem(
                title: "An unexpected error occurred",
                detail: exception?.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                instance: HttpContext.Request.Path,
                type: "https://metanit.com"
            );
        }
    }
}