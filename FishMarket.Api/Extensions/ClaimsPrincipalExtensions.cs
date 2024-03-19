using System.Security.Claims;
using System.Security.Principal;

namespace FishMarket.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetEmail(this IPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity as ClaimsIdentity;
        return identity?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public static bool IsRefreshToken(this IPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity as ClaimsIdentity;
        return identity?.FindFirst(ClaimTypes.AuthenticationMethod)?.Value == bool.TrueString;
    }
}
