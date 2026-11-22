using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PopPop.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Admin,Staff")]
public sealed class DashboardController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Orders", new { area = "Staff" });
    }
}
