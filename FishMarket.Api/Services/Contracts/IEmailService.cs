namespace FishMarket.Api.Services.Contracts;

public interface IEmailService
{
    Task SendAsync(string mailValidatorKey, string toEmail);
}
