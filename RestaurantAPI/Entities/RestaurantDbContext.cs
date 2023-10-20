using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities;

public class RestaurantDbContext : DbContext
{
    private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=RestaurantDb;Trusted_Connection=True";
        
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(25);

        modelBuilder.Entity<Dish>()
            .Property(d => d.Name)
            .IsRequired();
        
        modelBuilder.Entity<Address>(a =>
        {
            a.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(50);
            
            a.Property(s => s.Street)
                .IsRequired()
                .HasMaxLength(50);
            
            a.Property(p => p.PostalCode)
                .IsRequired()
                .HasMaxLength(6);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}