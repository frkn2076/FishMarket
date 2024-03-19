using FishMarket.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FishMarket.Api.Data;

public class MarketDbContext : DbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Fish> Fishes { get; set; }
}
