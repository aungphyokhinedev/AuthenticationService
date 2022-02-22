using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationService;
public static partial class Extensions
{
    

    public static Claim Claim(this HttpContext context)
    {
        return (Claim)context.Items["claim"];
    }

    public static void setClaim(this ActionExecutingContext context, Claim data)
    {
        context.HttpContext.Items["claim"] = data;
    }

    public static void setToken(this HttpResponse response,string token)
    {
        response.Headers.Add("Authorization", "Bearer " + token);
        
    }
    


}

