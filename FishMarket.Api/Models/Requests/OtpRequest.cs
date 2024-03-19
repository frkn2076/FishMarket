using System.ComponentModel.DataAnnotations;

namespace FishMarket.Api.Models.Requests;

public class OtpRequest
{
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(50, MinimumLength = 6)]
    public string Password { get; set; }

    [StringLength(6, MinimumLength = 6)]
    public string Otp { get; set; }
}
