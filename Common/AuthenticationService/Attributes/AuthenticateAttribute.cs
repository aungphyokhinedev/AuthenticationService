using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TokenService;
using AplusExtension;
namespace AuthenticationService;

public class AuthenticateAttribute : ActionFilterAttribute
{
    private IAuthenticate auth;

    public AuthenticateAttribute(IAuthenticate _auth)
    {
        auth = _auth;
    }


    public override async Task OnActionExecutionAsync(ActionExecutingContext context,
                                         ActionExecutionDelegate next)
    {

        try
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var claim = await auth.validateTokenAsync(token);
            context.setClaim(claim);
            await next();
        }
        catch (Exception ex)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

    }

}

