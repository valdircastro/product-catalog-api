using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // Optimization, does not track context entities great for read only queries
        try
        {
            var categoryList = _appDbContext.Categories.AsNoTracking().ToList();
            return Ok(categoryList);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpGet("products")]
    public ActionResult<IEnumerable<Category>> GetAllCategoriesAndProducts()
    {
        var categoryList = _appDbContext.Categories.Include(i => i.Products).ToList();
        return Ok(categoryList);
    }

    [HttpGet("{id:int}", Name = "GetCategoryById")]
    public ActionResult<Category> GetCategoryById(int id)
    {
        var category = _appDbContext.Categories.FirstOrDefault(p => p.CategoryId == id);

        if (category is null)
        {
            return NotFound("Category not found");
        }

        return category;
    }

    [HttpPost]
    public ActionResult<Category> AddCategory(Category? category)
    {
        if (category is null)
            return BadRequest();

        _appDbContext.Categories.Add(category);
        _appDbContext.SaveChanges();

        return new CreatedAtRouteResult(
            "GetCategoryById",
            new { id = category.CategoryId },
            category);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Category> UpdateCategory(int id, Category category)
    {
        if (id != category.CategoryId)
            return BadRequest("CategoryId does not exists");

        _appDbContext.Entry(category).State = EntityState.Modified;
        _appDbContext.SaveChanges();
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Category> DeleteCategory(int id)
    {
        var category = _appDbContext.Categories.FirstOrDefault(i => i.CategoryId == id);

        if (category is null)
            return NotFound("Category not found");
        
        _appDbContext.Categories.Remove(category);
        _appDbContext.SaveChanges();
        return Ok(category);
    }
}