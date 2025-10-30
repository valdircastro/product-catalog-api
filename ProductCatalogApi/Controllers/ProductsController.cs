using Microsoft.AspNetCore.Mvc;
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
        if (productList is null)
        {
            return NotFound("Products not found");
        }

        return Ok(productList);
    }
}
