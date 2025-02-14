using System.ComponentModel.DataAnnotations;

namespace assignment1.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    public decimal? Price { get; set; }
    
    public int? Quantity { get; set; }
    
    public string? Description { get; set; }
    
    // Foreign Key
    public int CategoryId { get; set; }
    
    // Navigation property
    // This property allows for easy access to the related Project entity from Task entity
    public Category? Category { get; set; }
    
    
    
    
}