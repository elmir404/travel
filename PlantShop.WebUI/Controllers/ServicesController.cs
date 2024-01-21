using Microsoft.AspNetCore.Mvc;

namespace PlantShop.WebUI.Controllers
{
	public class ServicesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
