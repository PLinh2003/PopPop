using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopPop.Data.Entities;

namespace PopPop.Areas.Identity.Pages.Account;

[AllowAnonymous]
public sealed class AdminLoginModel : PageModel
{
    private const string AdminRole = "Admin";
    private const string StaffRole = "Staff";
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AdminLoginModel(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ReturnUrl { get; set; }

    public sealed class InputModel
    {
        [Required(ErrorMessage = "Địa chỉ email không được để trống.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
        var user = await _userManager.GetUserAsync(User);
        if (user is not null)
        {
            var destination = await GetManagementDestinationAsync(user);
            if (destination is not null)
            {
                return Redirect(destination);
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không hợp lệ hoặc tài khoản không có quyền truy cập khu vực quản trị.");
            return Page();
        }

        var destination = await GetManagementDestinationAsync(user);
        if (destination is null)
        {
            ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không hợp lệ hoặc tài khoản không có quyền truy cập khu vực quản trị.");
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            destination = await GetManagementDestinationAsync(user);
            if (destination is not null)
            {
                return Redirect(destination);
            }

            await _signInManager.SignOutAsync();
            ModelState.AddModelError(string.Empty, "Tài khoản không có quyền truy cập khu vực quản trị.");
            return Page();
        }

        if (result.RequiresTwoFactor)
        {
            return RedirectToPage("./LoginWith2fa", new
            {
                ReturnUrl = destination,
                RememberMe = Input.RememberMe
            });
        }

        if (result.IsLockedOut)
        {
            return RedirectToPage("./Lockout");
        }

        ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không hợp lệ hoặc tài khoản không có quyền truy cập khu vực quản trị.");
        return Page();
    }

    private async Task<string?> GetManagementDestinationAsync(User user)
    {
        if (await _userManager.IsInRoleAsync(user, AdminRole))
        {
            return "/Admin";
        }

        return await _userManager.IsInRoleAsync(user, StaffRole) ? "/Staff" : null;
    }
}
