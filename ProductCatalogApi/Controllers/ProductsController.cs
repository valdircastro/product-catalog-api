using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Context;
using ProductCatalogApi.Models;

namespace ProductCatalogApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ProductsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        var productList = _appDbContext.Products.ToList();
        // This always returns a list, if no elements ef returns a [] list
        // It is safe to use count instead check null
        if (productList.Count == 0)
        {
            return NotFound("Products not found");
        }

        return Ok(productList);
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    public ActionResult<Product> GetProductById(int id)
    {
        var product = _appDbContext.Products.FirstOrDefault(p => p.ProductId == id);

        if (product is null)
        {
            return NotFound("Product not found");
        }

        return product;
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(Product? product)
    {
        if (product is null)
            return BadRequest();

        // Automatically verify the model and returns 400 if validations fails
        _appDbContext.Products.Add(product);
        _appDbContext.SaveChanges();

        return new CreatedAtRouteResult("GetProductById", new { id = product.ProductId }, product);
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateProductById(int id, Product product)
    {
        if (id != product.ProductId)
            return BadRequest("Payload product id differs from Route product id");

        // What this line does?
        _appDbContext.Entry(product).State = EntityState.Modified;
        _appDbContext.SaveChanges();

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteProductById(int id)
    {
        var product = _appDbContext.Products.FirstOrDefault(p => p.ProductId == id);

        if (product is null)
            return NotFound("Product not found");

        _appDbContext.Products.Remove(product);
        _appDbContext.SaveChanges();

        return Ok(product);
    }
}
