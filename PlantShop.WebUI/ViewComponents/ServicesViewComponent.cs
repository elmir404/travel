
using Microsoft.AspNetCore.Mvc;


namespace PlantShop.ViewComponents
{
	public class ServicesViewComponent : ViewComponent
	{


		public ServicesViewComponent()
		{

		}

		public async Task<IViewComponentResult> InvokeAsync()
		{


			return View();
		}
	}
}
