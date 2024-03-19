namespace FishMarket.Api.Extensions;

public static class HttpRequestExtensions
{
    public static bool IsRefreshTokenApiCall(this HttpRequest request)
    {
        const string refreshTokenEndpoint = "/authentication/refresh/";
        return request.Path.Value is not null
            && request.Path.Value.EndsWith(refreshTokenEndpoint);
    }
}
