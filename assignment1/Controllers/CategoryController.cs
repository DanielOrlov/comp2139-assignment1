using assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace assignment1.Controllers;

public class CategoryController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        // Database commande --> retrieve all categories from the Database
        var categories = new List<Category>()
        {
            new Category { CategoryId = 1, Name = "Category 1", Description = "Dummy category" }
        };
        
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category category)
    {
        //Database --> Persist new project to the database
        return RedirectToAction("Index");
    }
    
    //CRUD - Create - Read - Update - Delete

    public IActionResult Details(int id)
    {
        //Database --> Retrieve project from database
        var category = new Category(){CategoryId = id, Name = "Category 1", Description = "Dummy category" };
        return View(category);
    }
}