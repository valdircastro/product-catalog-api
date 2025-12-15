using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogApi.Context;
using ProductCatalogApi.Models;

namespace ProductCatalogApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public CategoriesController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetAllCategories()
    {
        var categoryList = _appDbContext.Categories.ToList();
        // This always returns a list, if no elements ef returns a [] list
        // It is safe to use count instead check null
        if (categoryList.Count == 0)
        {
            return NotFound("Products not found");
        }

        return Ok(categoryList);
    }
    
    [HttpGet("{id:int}", Name = "GetCategoryById")]
    public ActionResult<Category> GetCategoryById(int id)
    {
        var category = _appDbContext.Categories.FirstOrDefault(p => p.CategoryId == id);

        if (category is null)
        {
            return NotFound("Product not found");
        }

        return category;
    }
}
