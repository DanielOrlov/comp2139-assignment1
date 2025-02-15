using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assignment1.Controllers;
using assignment1.Data;
using assignment1.Models;

[Route("Product")]
public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index(int categoryId)
    {
        var products = _context
            .Products
            .Where(t => t.CategoryId == categoryId)
            .ToList();
        
        ViewBag.CategoryId = categoryId;
        return View(products);    
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var product = _context
            .Products.Include(p => p.Category)
            .FirstOrDefault(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpGet]
    public IActionResult Create(int categoryId)
    {
        var category = _context.Categories.Find(categoryId);
        if (category == null)
        {
            return NotFound();
        }

        var product = new Product
        {
            CategoryId = categoryId,
            Name = "",
            Description = "",
        };
        
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Title", "Description", "CategoryId")]Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index", new { categoryId = product.CategoryId });
        }
        
        return View(product);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _context
            .Products.Include(p => p.Category)
            .FirstOrDefault(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind( "ProductId", "Title", "Description", "CategoryId")] Product product)
    {
        if (id != product.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index", new { categoryId = product.CategoryId });
        }
        return View(product);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var product = _context
            .Products.Include(p => p.Category)
            .FirstOrDefault(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirm(int ProductId)
    {
        var product = _context.Products.Find(ProductId);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", new { categoryId = product.CategoryId });
        }
        return View(product);
    }
    
    [HttpGet("Search")]
    public async Task<IActionResult> Search(int? categoryId, string searchString)
    {
        var productsQuery = _context.Products.AsQueryable();

        bool searchPerformed = !string.IsNullOrEmpty(searchString);

        if (categoryId.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
        }

        if (searchPerformed)
        {
            searchString = searchString.ToLower();
            
            productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(searchString) ||
                                                         p.Description.ToLower().Contains(searchString));
            
        }
        
        // Asynchronous execution means this method does not block the thread while waiting for the database
        var products = await productsQuery.ToListAsync();
        
        // Pass search metadata
        ViewBag.CategoryId = categoryId;
        ViewData["SearchString"] = searchString;
        ViewData["SearcPerformed"] = searchPerformed;
        
        return View("Index", products);
    }
}