using System.ComponentModel.DataAnnotations;

namespace FishMarket.Api.Utils;

public record Smtp
{
    public Sender Sender { get; init; }
    public int Port { get; init; }
    public string Host { get; init; }
    public bool EnableSsl { get; init; }
}
