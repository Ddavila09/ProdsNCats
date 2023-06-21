using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdsNCats.Models;

namespace ProdsNCats.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext db;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    // ----------------------------------------------------------------------
    [HttpGet("")]
    public IActionResult AllProducts()
    {
        List<Product> allProducts = db.Products.ToList();
        return View("Index", allProducts);
    }
    // ----------------------------------------------------------------------
    [HttpPost("product/create")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        db.Products.Add(newProduct);
        db.SaveChanges();

        return RedirectToAction("Index");
    }
    // ----------------------------------------------------------------------

    [HttpGet("product/{ProductId}")]
    public IActionResult ViewProduct(int ProductId)
    {
        Product? product = db.Products.Include(product => product.AssCat).ThenInclude(cats => cats.Category).FirstOrDefault(product => product.ProductId == ProductId);

        if (product == null)
        {
            return RedirectToAction("Index");
        }

        List<Category> allCategories = db.Categories.Include(c => c.AssProd).Where(c => c.AssProd.All(ass => ass.ProductId != ProductId)).ToList();

        ViewBag.Categories = allCategories;

        return View("DetailsProd", product);
    }
    // ----------------------------------------------------------------------
    [HttpPost("product/assCreate")]
    public IActionResult CreateCatToProduct(Association newAss)
    {
        if (!ModelState.IsValid)
        {
            return View("DetailsProd");
        }

        bool AssExists = db.Associations.Any(a => a.ProductId == newAss.ProductId && a.CategoryId == newAss.CategoryId);
        if(AssExists)
        {
            return ViewProduct(newAss.ProductId);
        }

        db.Associations.Add(newAss);
        db.SaveChanges();

        return RedirectToAction("ViewProduct", new { ProductId = newAss.ProductId });
    }
    // ----------------------------------------------------------------------
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
