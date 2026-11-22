using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PopPop.Services.Interfaces;

namespace PopPop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IOrderService _orderService;

        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var model = await _orderService.GetAdminDashboardAsync(cancellationToken);
            return View(model);
        }
    }
}
