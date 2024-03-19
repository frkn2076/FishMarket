namespace FishMarket.Api.Utils;

public record JWTSettings
{
    public string SecretKey { get; init; }

    public int AccessTokenExpiresInHour { get; init; }

    public int RefreshTokenExpiresInHour { get; init; }
}
