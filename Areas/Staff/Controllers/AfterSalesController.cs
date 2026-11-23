using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopPop.Data;
using PopPop.Data.Entities;
using PopPop.Data.Entities.Enums;
using PopPop.Models;

namespace PopPop.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Admin,Staff")]
public sealed class AfterSalesController : Controller
{
    private readonly IDbContextFactory<PopPopDbContext> _contextFactory;

    public AfterSalesController(IDbContextFactory<PopPopDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Index(AfterSalesRequestStatus? status, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var query = context.AfterSalesRequests.AsNoTracking().AsQueryable();
        if (status.HasValue && Enum.IsDefined(status.Value)) query = query.Where(item => item.Status == status.Value);
        var requests = await query.Include(item => item.Order).Include(item => item.OrderItem)
            .Include(item => item.User).OrderByDescending(item => item.CreatedAt)
            .Select(item => new AfterSalesManagementRowViewModel
            {
                Id = item.Id,
                OrderCode = item.Order!.OrderCode,
                CustomerName = item.User!.FullName ?? item.User.Email ?? "Khách hàng",
                ProductName = item.OrderItem!.ProductName,
                Type = item.Type,
                Status = item.Status,
                Quantity = item.Quantity,
                CreatedAt = item.CreatedAt
            }).ToListAsync(cancellationToken);
        return View(new AfterSalesManagementListViewModel { Status = status, Requests = requests });
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var request = await context.AfterSalesRequests.AsNoTracking()
            .Include(item => item.Order).Include(item => item.OrderItem).Include(item => item.User)
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (request is null) return NotFound();
        var model = MapDetails(request);
        model.ReplacementVariants = await context.ProductVariants.AsNoTracking()
            .Where(item => item.IsActive && item.Product!.IsActive && item.StockQuantity >= request.Quantity)
            .OrderBy(item => item.Product!.Name).ThenBy(item => item.SKU)
            .Select(item => new AfterSalesReplacementOptionViewModel
            {
                Id = item.Id,
                Label = item.Product!.Name + " — " + (item.VariantLabel ?? item.SKU) + " (" + item.SKU + ")",
                StockQuantity = item.StockQuantity
            }).ToListAsync(cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, AfterSalesRequestStatus nextStatus, string? staffNote, bool restockReturnedItem, int? replacementProductVariantId, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(nextStatus) || (staffNote?.Length ?? 0) > 1000)
        {
            TempData["ErrorMessage"] = "Trạng thái hoặc ghi chú không hợp lệ.";
            return RedirectToAction(nameof(Details), new { id });
        }

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await using var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        var request = await context.AfterSalesRequests
            .Include(item => item.Order)
            .Include(item => item.OrderItem).ThenInclude(item => item!.ProductVariant)
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (request is null) return NotFound();

        var isAllowed = (request.Status, nextStatus) is
            (AfterSalesRequestStatus.Pending, AfterSalesRequestStatus.Approved or AfterSalesRequestStatus.Rejected)
            or (AfterSalesRequestStatus.Approved, AfterSalesRequestStatus.Completed);
        if (!isAllowed)
        {
            TempData["ErrorMessage"] = "Không thể chuyển yêu cầu sang trạng thái đã chọn.";
            return RedirectToAction(nameof(Details), new { id });
        }
        if (restockReturnedItem
            && (request.Type is not (AfterSalesRequestType.Return or AfterSalesRequestType.Exchange)
                || nextStatus != AfterSalesRequestStatus.Approved))
        {
            TempData["ErrorMessage"] = "Chỉ được nhập lại hàng khi duyệt yêu cầu đổi/trả.";
            return RedirectToAction(nameof(Details), new { id });
        }
        if (request.Type == AfterSalesRequestType.Exchange
            && nextStatus == AfterSalesRequestStatus.Completed
            && !replacementProductVariantId.HasValue)
        {
            TempData["ErrorMessage"] = "Chọn biến thể thay thế trước khi hoàn tất đổi hàng.";
            return RedirectToAction(nameof(Details), new { id });
        }
        if (replacementProductVariantId.HasValue
            && (request.Type != AfterSalesRequestType.Exchange || nextStatus != AfterSalesRequestStatus.Completed))
        {
            TempData["ErrorMessage"] = "Chỉ chọn hàng thay thế khi hoàn tất yêu cầu đổi hàng.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var now = DateTime.UtcNow;
        if (restockReturnedItem)
        {
            var variant = request.OrderItem?.ProductVariant;
            if (variant is null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy biến thể để cập nhật kho.";
                return RedirectToAction(nameof(Details), new { id });
            }
            variant.StockQuantity += request.Quantity;
            context.InventoryTransactions.Add(new InventoryTransaction
            {
                ProductVariantId = variant.Id,
                Type = InventoryTransactionType.AdjustmentIncrease,
                Quantity = request.Quantity,
                Note = $"Nhập lại hàng đổi/trả từ yêu cầu #{request.Id}",
                CreatedAt = now
            });
        }

        if (replacementProductVariantId.HasValue)
        {
            var replacementVariant = await context.ProductVariants.Include(item => item.Product)
                .FirstOrDefaultAsync(item => item.Id == replacementProductVariantId.Value, cancellationToken);
            if (replacementVariant is null || !replacementVariant.IsActive || replacementVariant.Product?.IsActive != true
                || replacementVariant.StockQuantity < request.Quantity
                || replacementVariant.Id == request.OrderItem?.ProductVariantId)
            {
                TempData["ErrorMessage"] = "Biến thể thay thế không hợp lệ, trùng sản phẩm gốc hoặc không đủ tồn kho.";
                return RedirectToAction(nameof(Details), new { id });
            }
            replacementVariant.StockQuantity -= request.Quantity;
            request.ReplacementProductVariantId = replacementVariant.Id;
            request.ReplacementSKU = replacementVariant.SKU;
            context.InventoryTransactions.Add(new InventoryTransaction
            {
                ProductVariantId = replacementVariant.Id,
                Type = InventoryTransactionType.AdjustmentDecrease,
                Quantity = -request.Quantity,
                Note = $"Xuất hàng thay thế từ yêu cầu #{request.Id}",
                CreatedAt = now
            });
        }

        request.Status = nextStatus;
        request.StaffNote = string.IsNullOrWhiteSpace(staffNote) ? null : staffNote.Trim();
        request.UpdatedAt = now;
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã cập nhật yêu cầu hậu mãi.";
        return RedirectToAction(nameof(Details), new { id });
    }

    private static AfterSalesManagementDetailsViewModel MapDetails(AfterSalesRequest request)
    {
        var statuses = request.Status switch
        {
            AfterSalesRequestStatus.Pending => new[] { AfterSalesRequestStatus.Approved, AfterSalesRequestStatus.Rejected },
            AfterSalesRequestStatus.Approved => new[] { AfterSalesRequestStatus.Completed },
            _ => []
        };
        return new AfterSalesManagementDetailsViewModel
        {
            Id = request.Id,
            OrderCode = request.Order?.OrderCode ?? string.Empty,
            CustomerName = request.User?.FullName ?? request.User?.Email ?? "Khách hàng",
            CustomerEmail = request.User?.Email ?? string.Empty,
            ProductName = request.OrderItem?.ProductName ?? string.Empty,
            SKU = request.OrderItem?.SKU ?? string.Empty,
            Type = request.Type,
            Status = request.Status,
            Quantity = request.Quantity,
            Reason = request.Reason,
            StaffNote = request.StaffNote,
            ReplacementSKU = request.ReplacementSKU,
            CreatedAt = request.CreatedAt,
            PaymentMethod = request.Order?.PaymentMethod ?? PaymentMethod.CashOnDelivery,
            AvailableStatuses = statuses
        };
    }
}
