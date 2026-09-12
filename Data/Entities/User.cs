using Microsoft.AspNetCore.Identity;

namespace PopPop.Data.Entities;

/// <summary>
/// Custom User extending IdentityUser
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Full name of the user
    /// </summary>
    public string? FullName { get; set; }
}
