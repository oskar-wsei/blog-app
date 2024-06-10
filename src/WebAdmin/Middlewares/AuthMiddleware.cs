using BlogApp.WebAdmin.Services;

namespace BlogApp.WebAdmin.Middlewares;

public class AuthMiddleware(WebAuthService authService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!IsRouteProtected(context.Request.Path) || await IsAuthenticated(context))
        {
            await next(context);
            return;
        }
        context.Response.Redirect("/Auth/SignIn");
    }

    private async Task<bool> IsAuthenticated(HttpContext context)
    {
        var token = context.Request.Cookies[Constants.AUTH_TOKEN];
        if (token is null) return false;
        return await authService.ValidateToken(token);
    }

    private bool IsRouteProtected(PathString pathString)
    {
        var path = pathString.Value ?? "";
        return !pathString.StartsWithSegments("/Auth") &&
               !path.EndsWith(".css") &&
               !path.EndsWith(".js") &&
               !path.EndsWith(".jpg") &&
               !path.EndsWith(".png") &&
               !path.EndsWith(".gif") &&
               !path.EndsWith(".svg") &&
               !path.EndsWith(".woff") &&
               !path.EndsWith(".woff2");
    }
}
