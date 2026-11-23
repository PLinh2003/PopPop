using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopPop.Data;
using PopPop.Data.Entities;
using PopPop.Data.Entities.Enums;
using PopPop.Models;

namespace PopPop.Controllers;

[Authorize]
public sealed class AfterSalesController : Controller
{
    private readonly IDbContextFactory<PopPopDbContext> _contextFactory;

    public AfterSalesController(IDbContextFactory<PopPopDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var requests = await context.AfterSalesRequests.AsNoTracking()
            .Where(item => item.UserId == userId)
            .Include(item => item.Order)
            .Include(item => item.OrderItem)
            .OrderByDescending(item => item.CreatedAt)
            .Select(item => new AfterSalesRequestRowViewModel
            {
                Id = item.Id,
                OrderCode = item.Order!.OrderCode,
                ProductName = item.OrderItem!.ProductName,
                SKU = item.OrderItem.SKU,
                Type = item.Type,
                Status = item.Status,
                Quantity = item.Quantity,
                Reason = item.Reason,
                StaffNote = item.StaffNote,
                ReplacementSKU = item.ReplacementSKU,
                CreatedAt = item.CreatedAt
            }).ToListAsync(cancellationToken);
        return View(new AfterSalesListViewModel { Requests = requests });
    }

    [HttpGet]
    public async Task<IActionResult> Create(int orderId, CancellationToken cancellationToken)
    {
        var model = await LoadCreateModelAsync(orderId, GetUserId(), cancellationToken);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AfterSalesCreateViewModel model, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await using var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        var order = await context.Orders.Include(item => item.OrderItems)
            .ThenInclude(item => item.ProductVariant).ThenInclude(item => item!.Product)
            .FirstOrDefaultAsync(item => item.Id == model.OrderId && item.UserId == userId, cancellationToken);
        if (order is null) return NotFound();

        var now = DateTime.UtcNow;
        var orderItem = order.OrderItems.FirstOrDefault(item => item.Id == model.OrderItemId);
        if (order.Status != OrderStatus.Completed || !order.CompletedAt.HasValue)
        {
            ModelState.AddModelError(string.Empty, "Chỉ đơn đã hoàn tất mới đủ điều kiện gửi yêu cầu.");
        }
        else if (orderItem is null)
        {
            ModelState.AddModelError(nameof(model.OrderItemId), "Sản phẩm không thuộc đơn hàng này.");
        }
        else if (!Enum.IsDefined(model.Type))
        {
            ModelState.AddModelError(nameof(model.Type), "Loại yêu cầu không hợp lệ.");
        }
        else if (model.Type is AfterSalesRequestType.Return or AfterSalesRequestType.Exchange
            && now > order.CompletedAt.Value.AddDays(7))
        {
            ModelState.AddModelError(string.Empty, "Yêu cầu đổi hàng/trả hàng chỉ được gửi trong 7 ngày kể từ khi đơn hoàn tất.");
        }
        else if (model.Type == AfterSalesRequestType.Warranty
            && (orderItem.ProductVariant?.Product?.WarrantyMonths is not > 0
                || now > order.CompletedAt.Value.AddMonths(orderItem.ProductVariant.Product.WarrantyMonths)))
        {
            ModelState.AddModelError(string.Empty, "Sản phẩm không có bảo hành hoặc đã hết hạn bảo hành.");
        }

        if (orderItem is not null && Enum.IsDefined(model.Type))
        {
            var activeRequests = context.AfterSalesRequests.Where(item => item.OrderItemId == orderItem.Id
                && item.Type == model.Type
                && (item.Status == AfterSalesRequestStatus.Pending || item.Status == AfterSalesRequestStatus.Approved));
            if (await activeRequests.AnyAsync(cancellationToken))
            {
                ModelState.AddModelError(string.Empty, "Đã có yêu cầu cùng loại đang được xử lý cho sản phẩm này.");
            }
            if (model.Type is AfterSalesRequestType.Return or AfterSalesRequestType.Exchange)
            {
                var allocatedQuantity = await context.AfterSalesRequests
                    .Where(item => item.OrderItemId == orderItem.Id
                        && (item.Type == AfterSalesRequestType.Return || item.Type == AfterSalesRequestType.Exchange)
                        && item.Status != AfterSalesRequestStatus.Rejected)
                    .SumAsync(item => (int?)item.Quantity, cancellationToken) ?? 0;
                if (model.Quantity + allocatedQuantity > orderItem.Quantity)
                    ModelState.AddModelError(nameof(model.Quantity), "Tổng số lượng yêu cầu đổi/trả vượt số lượng đã mua.");
            }
        }

        if (!ModelState.IsValid)
        {
            await transaction.RollbackAsync(cancellationToken);
            var refreshedModel = await LoadCreateModelAsync(model.OrderId, userId, cancellationToken);
            if (refreshedModel is null) return NotFound();
            refreshedModel.OrderItemId = model.OrderItemId;
            refreshedModel.Type = model.Type;
            refreshedModel.Quantity = model.Quantity;
            refreshedModel.Reason = model.Reason;
            return View(refreshedModel);
        }

        context.AfterSalesRequests.Add(new AfterSalesRequest
        {
            OrderId = order.Id,
            OrderItemId = orderItem!.Id,
            UserId = userId,
            Type = model.Type,
            Status = AfterSalesRequestStatus.Pending,
            Quantity = model.Quantity,
            Reason = model.Reason.Trim(),
            CreatedAt = now,
            UpdatedAt = now
        });
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã gửi yêu cầu. Nhân viên sẽ cập nhật kết quả xử lý.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<AfterSalesCreateViewModel?> LoadCreateModelAsync(int orderId, string userId, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var order = await context.Orders.AsNoTracking().Include(item => item.OrderItems)
            .ThenInclude(item => item.ProductVariant).ThenInclude(item => item!.Product)
            .FirstOrDefaultAsync(item => item.Id == orderId && item.UserId == userId, cancellationToken);
        if (order is null || order.Status != OrderStatus.Completed || !order.CompletedAt.HasValue) return null;
        return new AfterSalesCreateViewModel
        {
            OrderId = order.Id,
            OrderCode = order.OrderCode,
            Items = order.OrderItems.OrderBy(item => item.ProductName).Select(item => new AfterSalesOrderItemOptionViewModel
            {
                OrderItemId = item.Id,
                ProductName = item.ProductName,
                SKU = item.SKU,
                OrderedQuantity = item.Quantity,
                WarrantyMonths = item.ProductVariant?.Product?.WarrantyMonths ?? 0
            }).ToList()
        };
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("Không xác định được tài khoản hiện tại.");
}
