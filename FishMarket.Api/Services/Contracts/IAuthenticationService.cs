using FishMarket.Api.Models.DTOs;

namespace FishMarket.Api.Services.Contracts;

public interface IAuthenticationService
{
    AuthenticationModel GenerateToken(string email);
}
