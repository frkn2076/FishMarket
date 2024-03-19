using FishMarket.Api.Models.DTOs;
using FishMarket.Api.Models.Responses;

namespace FishMarket.Api.Mapper;

public static class MapperExtensions
{
    public static AuthenticationResponse MapToResponse(this AuthenticationModel authenticationModel)
    {
        if (authenticationModel is null)
        {
            return null;
        }

        return new()
        {
            AccessToken = authenticationModel.AccessToken,
            AccessTokenExpiresInHours = authenticationModel.AccessTokenExpiresInHours,
            RefreshToken = authenticationModel.RefreshToken,
            RefreshTokenExpiresInHours = authenticationModel.RefreshTokenExpiresInHours
        };
    }
}
