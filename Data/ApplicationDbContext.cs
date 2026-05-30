using Microsoft.EntityFrameworkCore;
using CrudApp.Models;

namespace CrudApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some initial data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop Pro",
                    Description = "High performance laptop with 16GB RAM",
                    Price = 1299.99m,
                    Quantity = 50,
                    Category = "Electronics",
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Product
                {
                    Id = 2,
                    Name = "Wireless Mouse",
                    Description = "Ergonomic wireless mouse with long battery life",
                    Price = 29.99m,
                    Quantity = 200,
                    Category = "Accessories",
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Product
                {
                    Id = 3,
                    Name = "USB-C Hub",
                    Description = "7-in-1 USB-C hub with HDMI, USB 3.0, and SD card reader",
                    Price = 49.99m,
                    Quantity = 150,
                    Category = "Accessories",
                    CreatedAt = new DateTime(2025, 1, 1)
                }
            );
        }
    }
}
