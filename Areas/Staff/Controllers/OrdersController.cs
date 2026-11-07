using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopPop.Data.Entities.Enums;
using PopPop.Services.Interfaces;

namespace PopPop.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Admin,Staff")]
public sealed class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(OrderStatus? status, string? search, CancellationToken cancellationToken)
    {
        var model = await _orderService.GetManagementOrdersAsync(status, search, cancellationToken);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var model = await _orderService.GetManagementOrderDetailsAsync(id, isAdmin: false, cancellationToken);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, OrderStatus nextStatus, CancellationToken cancellationToken)
    {
        try
        {
            await _orderService.UpdateOrderStatusAsync(id, nextStatus, isAdmin: false, cancellationToken);
            TempData["SuccessMessage"] = "Đã cập nhật trạng thái đơn hàng.";
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException exception)
        {
            TempData["ErrorMessage"] = exception.Message;
        }

        return RedirectToAction(nameof(Details), new { id });
    }
}
