using FishMarket.Api.Data;
using FishMarket.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FishMarket.Api.Middlewares;

public class AuthenticationMiddlewareForUserState(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, MarketDbContext marketDbContext)
    {
        if (!context.IsAnonymousAllowedApiCall())
        {
            var isInvalidUser = await marketDbContext.Users.AnyAsync(x =>
                x.Email == context.User.GetEmail()
                && !x.IsActive);

            if (isInvalidUser)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }
        
        await next(context);
    }
}
