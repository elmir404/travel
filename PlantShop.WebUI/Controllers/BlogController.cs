using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var blog = await _blogService.GetActiveBlogs();

          
            return View(blog);
        }

        public async Task<IActionResult> Detail(string slug)
        {
            var result = await _blogService.GetBlogBySlug(slug);
            if (result.Success)
                return View(result.Data);

            return BadRequest("Error occured");
        }
    }
}
