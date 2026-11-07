using System.ComponentModel.DataAnnotations;

namespace PopPop.Models;

public sealed class CreateStaffAccountViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(256, ErrorMessage = "Họ và tên không được vượt quá 256 ký tự.")]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
    [StringLength(256, ErrorMessage = "Email không được vượt quá 256 ký tự.")]
    [Display(Name = "Email nhân viên")]
    public string Email { get; set; } = string.Empty;
}

public sealed class StaffAccountListViewModel
{
    public IReadOnlyList<StaffAccountItemViewModel> Accounts { get; init; } = [];
}

public sealed class StaffAccountItemViewModel
{
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public bool HasPassword { get; init; }
}
