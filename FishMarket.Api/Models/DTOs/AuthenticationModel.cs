namespace FishMarket.Api.Models.DTOs;

public record AuthenticationModel
{
    public string AccessToken { get; set; }

    public int AccessTokenExpiresInHours { get; set; }

    public string RefreshToken { get; set; }

    public int RefreshTokenExpiresInHours { get; set; }
}
