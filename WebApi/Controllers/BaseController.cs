using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BaseController : ControllerBase
{
    protected string? GetErrorMessage
    {
        get
        {
            return ModelState.IsValid
                ? null
                : string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
        }
    }
}