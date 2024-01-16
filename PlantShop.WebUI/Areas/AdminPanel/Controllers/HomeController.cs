using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]

    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
