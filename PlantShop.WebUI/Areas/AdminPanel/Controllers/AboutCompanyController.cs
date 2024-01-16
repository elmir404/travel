using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Implementations.Main;

namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class AboutCompanyController : Controller
    {
        private readonly IAboutCompanyService _aboutCompanyService;

        public AboutCompanyController(IAboutCompanyService aboutCompanyService)
        {
            _aboutCompanyService = aboutCompanyService;
        }

        public async Task<IActionResult> List()
        {
            var aboutCompany = await _aboutCompanyService.GetAboutCompany();
            return View(aboutCompany.Data);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAboutCompany addAboutCompany)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _aboutCompanyService.AddAboutCompany(addAboutCompany);
            if (result.Success == false)
            {
                return View(addAboutCompany);
            }

            return Redirect("/AdminPanel/AboutCompany/List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _aboutCompanyService.GetAboutCompanyForUpdate(id);
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAboutCompany updateAboutCompany)
        {
            var result = await _aboutCompanyService.UpdateAboutCompany(updateAboutCompany);
            if (result.Success == false)
                return View(updateAboutCompany);

            return Redirect("/AdminPanel/AboutCompany/List");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _aboutCompanyService.GetAboutCompany(id);
            return View(result.Data);
        }
    }
}
