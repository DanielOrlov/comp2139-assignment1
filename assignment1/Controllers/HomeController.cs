using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using assignment1.Models;

namespace assignment1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult GeneralSearch(string searchType, string searchString)
    {
        if (searchType == "categories")
        {
            return RedirectToAction(nameof(CategoryController.Search), "Category", new {searchString = searchString});
        }

        else if (searchType == "products")
        {
            return RedirectToAction(nameof(ProductController.Search), "Product", new {searchString = searchString});
        }
        
        return RedirectToAction(nameof(Index), "Home");
    }
    
}