using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopPop.Services.Interfaces;

namespace PopPop.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var cart = await _cartService.GetCartAsync(GetUserId(), cancellationToken);
        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productVariantId, int quantity = 1, CancellationToken cancellationToken = default)
    {
        try
        {
            await _cartService.AddItemAsync(GetUserId(), productVariantId, quantity, cancellationToken);
            TempData["CartSuccess"] = "Đã thêm sản phẩm vào giỏ hàng.";
        }
        catch (InvalidOperationException exception)
        {
            TempData["CartError"] = exception.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _cartService.UpdateQuantityAsync(GetUserId(), cartItemId, quantity, cancellationToken);
            if (!updated)
            {
                TempData["CartError"] = "Không tìm thấy sản phẩm trong giỏ hàng.";
            }
        }
        catch (InvalidOperationException exception)
        {
            TempData["CartError"] = exception.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int cartItemId, CancellationToken cancellationToken)
    {
        var removed = await _cartService.RemoveItemAsync(GetUserId(), cartItemId, cancellationToken);
        TempData[removed ? "CartSuccess" : "CartError"] = removed
            ? "Đã xóa sản phẩm khỏi giỏ hàng."
            : "Không tìm thấy sản phẩm trong giỏ hàng.";
        return RedirectToAction(nameof(Index));
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("Không xác định được tài khoản hiện tại.");
    }
}
