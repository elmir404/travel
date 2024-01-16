using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Dtos.Get.Main;


namespace PlantShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAboutCompanyService _aboutCompanyService;
        private readonly ISliderService _sliderService;

        public HomeController(IProductService productService, IAboutCompanyService aboutCompanyService, ISliderService sliderService)
        {
            _productService = productService;
            _aboutCompanyService = aboutCompanyService;
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var slider = await _sliderService.GetSliders();
            ViewBag.Sliders = slider.Data;

            var products = await _productService.GetActiveProducts();
            return View(products.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAboutCompany()
        {
            var aboutCompany = await _aboutCompanyService.GetAboutCompany();
            return Ok(aboutCompany);
        }
    }
}
