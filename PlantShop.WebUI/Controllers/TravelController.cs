using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Services.Abstractions.Main;

namespace PlantShop.WebUI.Controllers
{
	public class TravelController : Controller
	{
		private readonly ISliderService _sliderService;

        public TravelController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
		{
			var result= await _sliderService.GetLastSlider();
			return View(result.Data);
		}
	}
}
