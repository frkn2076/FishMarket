using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishMarket.Api.Data.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(200)]
    public string Password { get; set; }

    [MaxLength(10)]
    public string Otp { get; set; }

    public bool IsActive { get; set; }
}
