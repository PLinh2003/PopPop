using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PopPop.Data.Entities;
using PopPop.Models;
using PopPop.Services.Interfaces;
using System.Diagnostics;

namespace PopPop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductService _productService;

        public HomeController(UserManager<User> userManager, IProductService productService)
        {
            _userManager = userManager;
            _productService = productService;
        }

        [HttpGet]
        [Route("home/about")]
        public IActionResult About()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var latestProducts = await _productService.GetLatestAvailableAsync(8, cancellationToken);
            return View(latestProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
