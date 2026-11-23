using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopPop.Data;
using PopPop.Data.Entities;
using PopPop.Data.Entities.Enums;
using PopPop.Models;

namespace PopPop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public sealed class VouchersController : Controller
{
    private readonly IDbContextFactory<PopPopDbContext> _contextFactory;

    public VouchersController(IDbContextFactory<PopPopDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var vouchers = await context.Vouchers.AsNoTracking().OrderByDescending(item => item.StartsAt)
            .Select(item => new VoucherManagementRowViewModel
            {
                Id = item.Id,
                Code = item.Code,
                DiscountType = item.DiscountType,
                DiscountValue = item.DiscountValue,
                UsedCount = item.UsedCount,
                MaximumUses = item.MaximumUses,
                StartsAt = item.StartsAt,
                ExpiresAt = item.ExpiresAt,
                IsActive = item.IsActive
            }).ToListAsync(cancellationToken);
        return View(new VoucherManagementListViewModel { Vouchers = vouchers });
    }

    [HttpGet]
    public IActionResult Create() => View("Form", new VoucherManagementFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VoucherManagementFormViewModel model, CancellationToken cancellationToken)
    {
        ValidateVoucher(model);
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var code = model.Code.Trim().ToUpperInvariant();
        if (await context.Vouchers.AnyAsync(item => item.Code == code, cancellationToken))
            ModelState.AddModelError(nameof(model.Code), "Mã voucher đã tồn tại.");
        if (!ModelState.IsValid) return View("Form", model);

        context.Vouchers.Add(new Voucher
        {
            Code = code,
            DiscountType = model.DiscountType,
            DiscountValue = model.DiscountValue,
            MinimumOrderAmount = model.MinimumOrderAmount,
            MaximumDiscountAmount = model.MaximumDiscountAmount,
            MaximumUses = model.MaximumUses,
            StartsAt = model.StartsAt.ToUniversalTime(),
            ExpiresAt = model.ExpiresAt.ToUniversalTime(),
            IsActive = model.IsActive
        });
        await context.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã tạo voucher.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var voucher = await context.Vouchers.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (voucher is null) return NotFound();
        return View("Form", new VoucherManagementFormViewModel
        {
            Id = voucher.Id,
            Code = voucher.Code,
            DiscountType = voucher.DiscountType,
            DiscountValue = voucher.DiscountValue,
            MinimumOrderAmount = voucher.MinimumOrderAmount,
            MaximumDiscountAmount = voucher.MaximumDiscountAmount,
            MaximumUses = voucher.MaximumUses,
            StartsAt = DateTime.SpecifyKind(voucher.StartsAt, DateTimeKind.Utc).ToLocalTime(),
            ExpiresAt = DateTime.SpecifyKind(voucher.ExpiresAt, DateTimeKind.Utc).ToLocalTime(),
            IsActive = voucher.IsActive
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, VoucherManagementFormViewModel model, CancellationToken cancellationToken)
    {
        model.Id = id;
        ValidateVoucher(model);
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var voucher = await context.Vouchers.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (voucher is null) return NotFound();
        var code = model.Code.Trim().ToUpperInvariant();
        if (model.MaximumUses.HasValue && model.MaximumUses.Value < voucher.UsedCount)
            ModelState.AddModelError(nameof(model.MaximumUses), "Giới hạn lượt dùng không được thấp hơn số lượt đã sử dụng.");
        if (await context.Vouchers.AnyAsync(item => item.Id != id && item.Code == code, cancellationToken))
            ModelState.AddModelError(nameof(model.Code), "Mã voucher đã tồn tại.");
        if (!ModelState.IsValid) return View("Form", model);

        voucher.Code = code;
        voucher.DiscountType = model.DiscountType;
        voucher.DiscountValue = model.DiscountValue;
        voucher.MinimumOrderAmount = model.MinimumOrderAmount;
        voucher.MaximumDiscountAmount = model.MaximumDiscountAmount;
        voucher.MaximumUses = model.MaximumUses;
        voucher.StartsAt = model.StartsAt.ToUniversalTime();
        voucher.ExpiresAt = model.ExpiresAt.ToUniversalTime();
        voucher.IsActive = model.IsActive;
        await context.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã cập nhật voucher.";
        return RedirectToAction(nameof(Index));
    }

    private void ValidateVoucher(VoucherManagementFormViewModel model)
    {
        if (!Enum.IsDefined(model.DiscountType)) ModelState.AddModelError(nameof(model.DiscountType), "Loại giảm giá không hợp lệ.");
        if (model.StartsAt >= model.ExpiresAt) ModelState.AddModelError(nameof(model.ExpiresAt), "Thời điểm kết thúc phải sau thời điểm bắt đầu.");
        if (model.DiscountType == VoucherDiscountType.Percentage && model.DiscountValue > 100)
            ModelState.AddModelError(nameof(model.DiscountValue), "Mức giảm theo phần trăm không được vượt quá 100%.");
        if (model.MaximumDiscountAmount.HasValue && model.DiscountType != VoucherDiscountType.Percentage)
            ModelState.AddModelError(nameof(model.MaximumDiscountAmount), "Giới hạn giảm chỉ dùng cho voucher theo phần trăm.");
    }
}
