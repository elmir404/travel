using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public BlogController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(SearchModelDto documentSearchModel, int currentPage = 1, int pageSize = 12)
        {
            var categories = await _categoryService.GetActiveCategories();
            var products = await _productService.SearchProduct(currentPage, pageSize, documentSearchModel);

            var viewmodel = new GetSearchDto()
            {
                Products = products.Data as SearchResult<IEnumerable<GetProductDto>>,
                Categories = categories.Data as List<GetCategoryDto>,
                categoryId = documentSearchModel.categoryIds?.FirstOrDefault(),
            };
            return View(viewmodel);
        }

        public async Task<IActionResult> Detail(string slug)
        {
            var result = await _productService.GetProductBySlug(slug);
            if (result.Success)
                return View(result.Data);

            return BadRequest("Error occured");
        }
    }
}
