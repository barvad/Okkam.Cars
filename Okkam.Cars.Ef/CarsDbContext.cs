using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Okkam.Cars.Ef.Entities;
using Okkam.Cars.Ef.EntitiesConfigurations;

namespace Okkam.Cars.Ef;

public class CarsDbContext : DbContext
{
    public CarsDbContext()
    {
       
    }

    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<BodyStyleEntity> BodyStyles { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }
    private void OnSavingChanges(object? sender, SavingChangesEventArgs e)
    {
        foreach (var entry in ChangeTracker.Entries<CarEntity>())
        {
            var now = DateTime.UtcNow;
            if (entry.State is EntityState.Added) entry.Entity.Created = now;
        }
    }
    public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
    {
        Database.EnsureCreated();
        SavingChanges += OnSavingChanges;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CarsEntityConfiguration());
        modelBuilder.UseIdentityColumns();
        modelBuilder.ApplyConfiguration(new BodyStyleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BrandEntityConfiguration());
        modelBuilder.Entity<BodyStyleEntity>().HasData(new BodyStyleEntity[]
        {
            new() { Id = 1, Name = "Седан" },
            new() { Id = 2, Name = "Хэтчбек" },
            new() { Id = 3, Name = "Универсал" },
            new() { Id = 4, Name = "Минивэн" },
            new() { Id = 5, Name = "Внедорожник" },
            new() { Id = 6, Name = "Купе" }
        });
        modelBuilder.Entity<BrandEntity>().HasData(new BrandEntity[]
        {
            new() { Id = 1, Name = "Audi" },
            new() { Id = 2, Name = "Ford" },
            new() { Id = 3, Name = "Jeep" },
            new() { Id = 4, Name = "Nissan" },
            new() { Id = 5, Name = "Toyota" }
        });
     
    }

    
}   