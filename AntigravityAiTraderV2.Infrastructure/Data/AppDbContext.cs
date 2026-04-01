using AntigravityAiTraderV2.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AntigravityAiTraderV2.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Signal> Signals { get; set; }
    public DbSet<MarketData> MarketData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Signal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Symbol).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Confidence).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<MarketData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Symbol).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18,4)");
            entity.Property(e => e.Volume).HasColumnType("decimal(18,4)");
        });
    }
}
