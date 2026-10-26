using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopPop.Models;
using PopPop.Services.Interfaces;

namespace PopPop.Controllers;

[AllowAnonymous]
public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetDetailsAsync(id, cancellationToken);
        return product is null ? NotFound() : View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Search(string q, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return PartialView("_SearchResults", Array.Empty<ProductCardViewModel>());
        }

        var results = await _productService.SearchAsync(q, 6, cancellationToken);
        return PartialView("_SearchResults", results);
    }
} 
