using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using PopPop.Data.Entities;
using PopPop.Models;

namespace PopPop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public sealed class StaffAccountsController : Controller
{
    private const string StaffRole = "Staff";

    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<StaffAccountsController> _logger;

    public StaffAccountsController(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IEmailSender emailSender,
        ILogger<StaffAccountsController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailSender = emailSender;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.GetUsersInRoleAsync(StaffRole);
        var accounts = new List<StaffAccountItemViewModel>(users.Count);
        foreach (var user in users.OrderBy(user => user.FullName ?? user.Email))
        {
            accounts.Add(new StaffAccountItemViewModel
            {
                FullName = user.FullName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                HasPassword = await _userManager.HasPasswordAsync(user)
            });
        }

        return View(new StaffAccountListViewModel { Accounts = accounts });
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateStaffAccountViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateStaffAccountViewModel model)
    {
        model.FullName = model.FullName?.Trim() ?? string.Empty;
        model.Email = model.Email?.Trim() ?? string.Empty;
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!await _roleManager.RoleExistsAsync(StaffRole))
        {
            ModelState.AddModelError(string.Empty, "Không tìm thấy vai trò Staff. Vui lòng kiểm tra dữ liệu phân quyền.");
            return View(model);
        }

        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName,
            EmailConfirmed = true
        };

        var createResult = await _userManager.CreateAsync(user);
        if (!createResult.Succeeded)
        {
            AddIdentityErrors(createResult);
            return View(model);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, StaffRole);
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            AddIdentityErrors(roleResult);
            return View(model);
        }

        try
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));
            var setupUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code = encodedToken, email = user.Email },
                protocol: Request.Scheme)
                ?? throw new InvalidOperationException("Không thể tạo liên kết đặt mật khẩu.");

            var safeName = HtmlEncoder.Default.Encode(user.FullName ?? string.Empty);
            var safeUrl = HtmlEncoder.Default.Encode(setupUrl);
            var message = $"<p>Xin chào {safeName},</p><p>Tài khoản nhân viên PôpPôp đã được tạo cho bạn. Hãy mở liên kết bên dưới để tự đặt mật khẩu và bắt đầu sử dụng hệ thống:</p><p style='margin:24px 0;'><a href='{safeUrl}' style='display:inline-block;padding:12px 22px;background-color:#2563eb;color:#ffffff;text-decoration:none;border-radius:6px;font-weight:700;'>Đặt mật khẩu tài khoản</a></p><p>Nếu bạn không mong đợi email này, vui lòng bỏ qua. Không chia sẻ liên kết này với người khác.</p>";
            await _emailSender.SendEmailAsync(user.Email!, "Kích hoạt tài khoản nhân viên PôpPôp", message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to send staff account setup email to {Email}", user.Email);
            await _userManager.RemoveFromRoleAsync(user, StaffRole);
            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                _logger.LogError("Could not remove staff account {UserId} after invitation email failed", user.Id);
                TempData["ErrorMessage"] = "Không gửi được email mời. Tài khoản chưa có mật khẩu nhưng chưa thể tự động xóa; vui lòng kiểm tra cấu hình email và xử lý tài khoản này trước khi tạo lại.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Không gửi được email mời nên tài khoản chưa được tạo. Vui lòng kiểm tra cấu hình Email:Username và Email:Password.");
            return View(model);
        }

        TempData["SuccessMessage"] = $"Đã tạo tài khoản nhân viên cho {user.Email} và gửi liên kết đặt mật khẩu.";
        return RedirectToAction(nameof(Index));
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
