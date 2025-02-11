using System.ComponentModel.DataAnnotations;

namespace assignment1.Models;

public class Category
{
    /// <summary>
    /// This is the primary key for categories
    /// </summary>
    public int CategoryId { get; set; }
    
    /// <summary>
    /// The Name of the category
    /// [Required]: Ensures that property must be set -> must have a category name
    /// </summary>
    [Required]
    public required string Name { get; set; }
    
    public string? Description { get; set; }
    
    
    
    // [DataType(DataType.Date)]
    // public DateTime StartDate { get; set; }
    //
    // [DataType(DataType.Date)]
    // public DateTime EndDate { get; set; }
    //
    // public string? Status { get; set; }
    
    // One-to-Many
    // This will allow EF Core to understand that one Project has potentially many ProjectTasks
    //public List<ProjectTask>? ProjectTasks { get; set; }
}