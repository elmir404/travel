using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Implementations.Main;
using System.Data;

namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]

    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> List()
        {
            var about = await _aboutService.GetAbout();
            return View(about.Data);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAboutDto addAboutDto)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _aboutService.AddAbout(addAboutDto);
            if (result.Success == false)
            {
                return View(addAboutDto);
            }

            return Redirect("/AdminPanel/About/List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _aboutService.GetAboutForUpdate(id);
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAboutDto updateAboutDto)
        {
            var result = await _aboutService.UpdateAbout(updateAboutDto);
            if (result.Success == false)
                return View(updateAboutDto);

            return Redirect("/AdminPanel/About/List");
        }
    }
}
