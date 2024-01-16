using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Services.Abstractions.Main;
using System.Data;

namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetActiveCategories();
            return View(categories.Data);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            var result = await _categoryService.AddCategory(model);
            if (result.Success == false)
            {
                return View(model);
            }

            return Redirect("/AdminPanel/Category/List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetCategory(id);
            return View(category.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddCategoryDto model, int id)
        {
            var result = await _categoryService.UpdateCategory(model, id);
            if (result.Success == false)
                return View(model);

            return Redirect("/AdminPanel/Category/List");
        }

        public async Task<IActionResult> DeactiveCategory()
        {
            var result = await _categoryService.GetDeactiveCategories();
            if (result.Success)
                return Ok(result);

            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> Deactive(int id)
        {
            var result = await _categoryService.DeactiveCategory(id);
            if (result.Success)
                return Ok(result);

            return BadRequest("Error occured");
        }

        [HttpPatch]
        public async Task<IActionResult> Active(int id)
        {
            var result = await _categoryService.ActiveCategory(id);
            if (result.Success)
                return Ok(result);

            return BadRequest("Error occured");
        }
    }
}
