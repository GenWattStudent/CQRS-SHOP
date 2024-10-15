using CarShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Shared.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase(Constants.CarShopDatabaseName);
    }
}

