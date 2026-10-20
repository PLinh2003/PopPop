using Microsoft.AspNetCore.Identity;

namespace PopPop.Data;

public sealed class VietnameseIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => CreateError(nameof(DefaultError), "Đã xảy ra lỗi không xác định.");

    public override IdentityError ConcurrencyFailure() => CreateError(nameof(ConcurrencyFailure), "Dữ liệu đã được thay đổi bởi người khác. Vui lòng tải lại và thử lại.");

    public override IdentityError PasswordMismatch() => CreateError(nameof(PasswordMismatch), "Mật khẩu không chính xác.");

    public override IdentityError InvalidToken() => CreateError(nameof(InvalidToken), "Mã xác minh không hợp lệ hoặc đã hết hạn.");

    public override IdentityError LoginAlreadyAssociated() => CreateError(nameof(LoginAlreadyAssociated), "Tài khoản đăng nhập này đã được liên kết với người dùng khác.");

    public override IdentityError InvalidUserName(string? userName) => CreateError(nameof(InvalidUserName), "Tên đăng nhập không hợp lệ.");

    public override IdentityError InvalidEmail(string? email) => CreateError(nameof(InvalidEmail), "Địa chỉ email không hợp lệ.");

    public override IdentityError DuplicateUserName(string userName) => CreateError(nameof(DuplicateUserName), "Tên đăng nhập này đã được sử dụng.");

    public override IdentityError DuplicateEmail(string email) => CreateError(nameof(DuplicateEmail), "Địa chỉ email này đã được sử dụng.");

    public override IdentityError InvalidRoleName(string? role) => CreateError(nameof(InvalidRoleName), "Tên vai trò không hợp lệ.");

    public override IdentityError DuplicateRoleName(string role) => CreateError(nameof(DuplicateRoleName), "Tên vai trò này đã được sử dụng.");

    public override IdentityError UserAlreadyHasPassword() => CreateError(nameof(UserAlreadyHasPassword), "Tài khoản đã có mật khẩu.");

    public override IdentityError UserLockoutNotEnabled() => CreateError(nameof(UserLockoutNotEnabled), "Tính năng khóa tài khoản chưa được bật.");

    public override IdentityError UserAlreadyInRole(string role) => CreateError(nameof(UserAlreadyInRole), "Người dùng đã thuộc vai trò này.");

    public override IdentityError UserNotInRole(string role) => CreateError(nameof(UserNotInRole), "Người dùng không thuộc vai trò này.");

    public override IdentityError PasswordTooShort(int length) => CreateError(nameof(PasswordTooShort), $"Mật khẩu phải có ít nhất {length} ký tự.");

    public override IdentityError PasswordRequiresNonAlphanumeric() => CreateError(nameof(PasswordRequiresNonAlphanumeric), "Mật khẩu phải chứa ít nhất một ký tự đặc biệt.");

    public override IdentityError PasswordRequiresDigit() => CreateError(nameof(PasswordRequiresDigit), "Mật khẩu phải chứa ít nhất một chữ số.");

    public override IdentityError PasswordRequiresLower() => CreateError(nameof(PasswordRequiresLower), "Mật khẩu phải chứa ít nhất một chữ cái thường.");

    public override IdentityError PasswordRequiresUpper() => CreateError(nameof(PasswordRequiresUpper), "Mật khẩu phải chứa ít nhất một chữ cái in hoa.");

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => CreateError(nameof(PasswordRequiresUniqueChars), $"Mật khẩu phải chứa ít nhất {uniqueChars} ký tự khác nhau.");

    private static IdentityError CreateError(string code, string description) => new()
    {
        Code = code,
        Description = description
    };
}
