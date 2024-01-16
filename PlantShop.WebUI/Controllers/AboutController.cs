using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Services.Abstractions.Main;

namespace PlantShop.WebUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAbout();
            return View(abouts.Data);
        }
        public async Task<IActionResult> HowItWorks()
        {
           
            return View();
        }
    }
}
