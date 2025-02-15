using assignment1.Data;
using assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assignment1.Controllers;

[Route("Category")]
public class CategoryController : Controller
{
    
    private readonly ApplicationDbContext _context;
    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Index action will retrieve a listing of categories (database)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        // Retrieve all categories from the database
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken] // Add this to every post request
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category); // Add new category to database
            _context.SaveChanges();            // Save changes to the database
            return RedirectToAction("Index");
        }
        return View(category);
    }
    
    //CRUD - Create - Read - Update - Delete

    [HttpGet]
    public IActionResult Details(int id)
    {
        //Database --> Retrieve category with the specified id or returns null if not found
        var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("CategoryId, Name, Description")] Category category)
    {
        // [Bind] ensure only the specified properties are updated
        if (id != category.CategoryId)
        {
            return NotFound(); // ensures that in the route matches the ID model
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Categories.Update(category); // Update the category
                _context.SaveChanges();               // Commit changes
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        return View(category);
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.CategoryId == id);
    }
    
    [HttpGet]
    public IActionResult Delete(int id)
    {
        //Database --> Retrieve category with the specified id or returns null if not found
        var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int CategoryId)
    {
        var category = _context.Categories.Find(CategoryId);
        if (category != null)
        {
           _context.Categories.Remove(category); // Remove category from database
           _context.SaveChanges();               // commit changes to the database
           return RedirectToAction("Index");
        }
        return NotFound();
    }

    [HttpGet("Search/{searchString}")]
    public async Task<IActionResult> Search(string searchString)
    {
        var categoriesQuery = _context.Categories.AsQueryable();

        bool searchPerformed = !string.IsNullOrEmpty(searchString);

        if (searchPerformed)
        {
            searchString = searchString.ToLower();
            
            categoriesQuery = categoriesQuery.Where(c => c.Name.ToLower().Contains(searchString) ||
                                                         c.Description.ToLower().Contains(searchString));
            
        }
        
        // Asynchronous execution means this method does not block the thread while waiting for the database
        var categories = await categoriesQuery.ToListAsync();
        
        // Pass search metadata
        ViewData["SearchString"] = searchString;
        ViewData["SearcPerformed"] = searchPerformed;
        
        return View("Index", categories);
    }
    
}