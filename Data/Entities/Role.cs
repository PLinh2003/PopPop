using Microsoft.AspNetCore.Identity;

namespace PopPop.Data.Entities;

/// <summary>
/// Custom Role extending IdentityRole
/// </summary>
public class Role : IdentityRole
{
    /// <summary>
    /// Description of the role
    /// </summary>
    public string? Description { get; set; }
}
