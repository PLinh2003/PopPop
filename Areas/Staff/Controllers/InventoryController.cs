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
public sealed class InventoryController : Controller
{
    private readonly IDbContextFactory<PopPopDbContext> _contextFactory;

    public InventoryController(IDbContextFactory<PopPopDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? search, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var variantsQuery = context.ProductVariants.AsNoTracking().Include(item => item.Product).AsQueryable();
        var transactionsQuery = context.InventoryTransactions.AsNoTracking()
            .Include(item => item.ProductVariant).ThenInclude(item => item!.Product).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            variantsQuery = variantsQuery.Where(item => item.SKU.Contains(term) || item.Product!.Name.Contains(term));
            transactionsQuery = transactionsQuery.Where(item => item.ProductVariant!.SKU.Contains(term)
                || item.ProductVariant.Product!.Name.Contains(term));
        }

        var model = new InventoryManagementViewModel
        {
            Search = search,
            Variants = await variantsQuery.OrderBy(item => item.Product!.Name).ThenBy(item => item.SKU)
                .Select(item => new InventoryVariantRowViewModel
                {
                    Id = item.Id,
                    ProductName = item.Product!.Name,
                    SKU = item.SKU,
                    Label = item.VariantLabel,
                    StockQuantity = item.StockQuantity,
                    IsActive = item.IsActive
                }).ToListAsync(cancellationToken),
            Transactions = await transactionsQuery.OrderByDescending(item => item.CreatedAt).Take(100)
                .Select(item => new InventoryTransactionRowViewModel
                {
                    CreatedAt = item.CreatedAt,
                    ProductName = item.ProductVariant!.Product!.Name,
                    SKU = item.ProductVariant.SKU,
                    Type = item.Type,
                    Quantity = item.Quantity,
                    Note = item.Note
                }).ToListAsync(cancellationToken)
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adjust(int productVariantId, InventoryTransactionType type, int quantity, string? note, string? search, CancellationToken cancellationToken)
    {
        if (quantity <= 0 || (note?.Length ?? 0) > 500
            || type is not (InventoryTransactionType.Import or InventoryTransactionType.AdjustmentIncrease or InventoryTransactionType.AdjustmentDecrease))
        {
            TempData["ErrorMessage"] = "Loại giao dịch hoặc số lượng điều chỉnh không hợp lệ.";
            return RedirectToAction(nameof(Index), new { search });
        }

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await using var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        var variant = await context.ProductVariants.FirstOrDefaultAsync(item => item.Id == productVariantId, cancellationToken);
        if (variant is null) return NotFound();

        var signedQuantity = type == InventoryTransactionType.AdjustmentDecrease ? -quantity : quantity;
        if (variant.StockQuantity + signedQuantity < 0)
        {
            TempData["ErrorMessage"] = "Số lượng giảm vượt quá tồn kho hiện tại.";
            return RedirectToAction(nameof(Index), new { search });
        }

        variant.StockQuantity += signedQuantity;
        context.InventoryTransactions.Add(new InventoryTransaction
        {
            ProductVariantId = variant.Id,
            Type = type,
            Quantity = signedQuantity,
            Note = string.IsNullOrWhiteSpace(note) ? "Điều chỉnh tồn kho thủ công" : note.Trim(),
            CreatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã cập nhật tồn kho và ghi lịch sử giao dịch.";
        return RedirectToAction(nameof(Index), new { search });
    }
}
