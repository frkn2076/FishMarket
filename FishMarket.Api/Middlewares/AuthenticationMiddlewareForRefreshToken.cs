using FishMarket.Api.Extensions;

namespace FishMarket.Api.Middlewares;

public class AuthenticationMiddlewareForRefreshToken(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.IsRefreshTokenApiCall()
            && context.User.IsRefreshToken())
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await next(context);
    }
}
