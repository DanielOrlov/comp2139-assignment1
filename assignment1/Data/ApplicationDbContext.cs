using assignment1.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define one-to-many relationship
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)         // One categorie has (potentially) many products
            .WithOne(p => p.Category)          // Each Product belongs to one Category
            .HasForeignKey(p => p.CategoryId)  // Foreign Key in Products table
            .OnDelete(DeleteBehavior.Cascade);        // Cascade delete Products when Category is deleted
        
        // Seeding the database
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Electronics", Description = "Electronic devices and appliances"},
            new Category { CategoryId = 2, Name = "Accessories", Description = "Keyboards, mouses, phone cases, you name it"}
        );


    }
}