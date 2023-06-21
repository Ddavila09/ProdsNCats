using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProdsNCats.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProdsNCats.Controllers;

public class CatController : Controller
{
    private readonly ILogger<CatController> _logger;
    private MyContext db;

    public CatController(ILogger<CatController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    // -------------------------------------------------------

    [HttpGet("/category")]
    public IActionResult AllCats()
    {
        List<Category> allCats = db.Categories.ToList();
        return View("Category", allCats);
    }
    // ----------------------------------------------------------------------
    [HttpPost("categories/create")]
    public IActionResult CreateCategory(Category newCategory)
    {
        if (!ModelState.IsValid)
        {
            // return AllCats(); - can also do it
            List<Category> allCats = db.Categories.ToList();
            return View("Category", allCats);
        }

        db.Categories.Add(newCategory);
        db.SaveChanges();

        return RedirectToAction("AllCats");
    }
    // ----------------------------------------------------------------------
    [HttpGet("categories/{categoryId}")]
    public IActionResult ViewCategory(int CategoryId)
    {
        Category? category = db.Categories.Include(category => category.AssProd).ThenInclude(prods => prods.Product).FirstOrDefault(category => category.CatId == CategoryId);

        if (category == null)
        {
            return RedirectToAction("Category");
        }

        List<Product> allProducts = db.Products.Include(c => c.AssCat).Where(c => c.AssCat.All(ass => ass.CategoryId != CategoryId)).ToList();

        ViewBag.Products = allProducts;

        return View("DetailsCat", category);
    }
    // ----------------------------------------------------------------------
    [HttpPost("categories/assCreate")]
    public IActionResult CreateProdToCat(Association newProd)
    {
        if (!ModelState.IsValid)
        {
            return ViewCategory(newProd.CategoryId);
        }

        bool AssExists = db.Associations.Any(a => a.CategoryId == newProd.CategoryId && a.CategoryId == newProd.ProductId);
        if(AssExists)
        {
            return ViewCategory(newProd.CategoryId);
        }

            db.Associations.Add(newProd);
            db.SaveChanges();

        return RedirectToAction("ViewCategory", new { CategoryId = newProd.CategoryId });
    }
}