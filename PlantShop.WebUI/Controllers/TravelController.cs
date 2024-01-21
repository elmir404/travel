using Microsoft.AspNetCore.Mvc;

namespace PlantShop.WebUI.Controllers
{
	public class TravelController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
