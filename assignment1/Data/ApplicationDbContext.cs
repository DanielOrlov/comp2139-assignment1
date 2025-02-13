using assignment1.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Category> Categories { get; set; }
}