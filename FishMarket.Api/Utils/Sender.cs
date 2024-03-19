using System.ComponentModel.DataAnnotations;

namespace FishMarket.Api.Utils;

public record Sender
{
    public string Email { get; init; }
    public string Password { get; init; }
}
