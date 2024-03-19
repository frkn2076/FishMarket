namespace FishMarket.Api.Models.Responses;

public class AuthenticationResponse
{
    public string AccessToken { get; set; }

    public int AccessTokenExpiresInHours { get; set; }

    public string RefreshToken { get; set; }

    public int RefreshTokenExpiresInHours { get; set; }

    public string GrantType { get; set; } = "Bearer";
}
