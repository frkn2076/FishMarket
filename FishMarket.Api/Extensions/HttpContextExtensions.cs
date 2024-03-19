using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;

namespace FishMarket.Api.Extensions;

public static class HttpContextExtensions
{
    public static bool IsAnonymousAllowedApiCall(this HttpContext httpContext)
    {
        return httpContext.Features.Get<IEndpointFeature>()?
            .Endpoint?
            .Metadata?
            .Any(x => x is AllowAnonymousAttribute) ?? false;
    }
}
