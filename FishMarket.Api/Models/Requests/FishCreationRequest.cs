using System.ComponentModel.DataAnnotations;

namespace FishMarket.Api.Models.Requests;

public class FishCreationRequest
{
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }

    [Range(0, double.MaxValue, MinimumIsExclusive = true)]
    public decimal Price { get; set; }
}
