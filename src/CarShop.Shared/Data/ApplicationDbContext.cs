using CarShop.Domain.Entities;
using CarShop.Domain.ValueObjects;
using CarShop.Shared.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Shared.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<CarEntity> Cars { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase(Constants.CarShopDatabaseName);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CarEntity>(ConfigureCar);
    }

    private void ConfigureCar(EntityTypeBuilder<CarEntity> builder)
    {
        builder.Property(c => c.Brand).IsRequired().HasMaxLength(100);
        builder.Property(c => c.VIN).IsRequired().HasMaxLength(17);
    }
}

