
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Services.Abstractions.Main;

namespace PlantShop.ViewComponents
{
	public class BlogViewComponent : ViewComponent
	{
		private readonly IBlogService _blogService;

		public BlogViewComponent(IBlogService blogService)
		{
			_blogService = blogService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var response = await _blogService.SearchBlogs(1, 3);
			
			return View(response.Data);
		}
	}
}