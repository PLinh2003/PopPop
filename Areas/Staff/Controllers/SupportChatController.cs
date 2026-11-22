using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PopPop.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Admin,Staff")]
public sealed class SupportChatController : Controller
{
    public IActionResult Index() => View();
}
