
using Microsoft.AspNetCore.Mvc;


namespace PlantShop.ViewComponents
{
	public class BlogViewComponent : ViewComponent
	{
		

		public BlogViewComponent()
		{
			
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{

			
			return View();
		}
	}
}