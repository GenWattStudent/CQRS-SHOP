using CarShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Shared.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase(Constants.CarShopDatabaseName);
    }
}

