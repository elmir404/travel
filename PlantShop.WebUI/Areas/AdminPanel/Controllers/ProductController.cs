using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Services.Abstractions.Main;


namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductFileService _productFileService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, IProductFileService productFileService, ICategoryService categoryService)
        {
            _productService = productService;
            _productFileService = productFileService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> List()
        {
            var result = await _productService.GetActiveProducts();
            return View(result.Data);
        }
        public async Task<IActionResult> Add()
        {
            var respons = await _categoryService.GetActiveCategories();
            return View(respons.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductDto model)
        {
            var respons = await _categoryService.GetActiveCategories();
            if (!ModelState.IsValid)
            {

                foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(respons.Data);

            }
            var result = await _productService.AddProduct(model);
            if (result.Success)
            {
                model.Files = Request.Form.Files;
                await _productFileService.AddRangeAsync(model, (int)result.Data);
                return Redirect("/AdminPanel/Product/List");
            }
            ModelState.AddModelError("", "Bu adda məhsul var");
            return View(respons.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var categories = await _categoryService.GetActiveCategories();
            ViewBag.Categories = categories.Data;
            var product = await _productService.GetProductForUpdate(id);

            return View(product.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            var categories = await _categoryService.GetActiveCategories();
            ViewBag.Categories = categories.Data;

            var result = await _productService.UpdateProduct(updateProductDto);
            if (result.Success && updateProductDto.Files != null)
            {
                updateProductDto.Files = Request.Form.Files;
                await _productFileService.UpdateRangeAsync(updateProductDto);
                return Redirect("/AdminPanel/Product/List");
            }

            return Redirect("/AdminPanel/Product/List");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

        public async Task<IActionResult> Deactive()
        {
            var result = await _productService.GetDeactiveProducts();
            if (result.Success)
                return View(result.Data);

            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> Deactive(int id)
        {
            var result = await _productService.DeactiveProduct(id);
            if (result.Success)
                return Ok(result);

            return BadRequest("Error occured");
        }

        [HttpPatch]
        public async Task<IActionResult> Active(int id)
        {
            var result = await _productService.ActiveProduct(id);
            if (result.Success)
                return Ok(result);

            return BadRequest("Error occured");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _productService.GetProduct(id);
            if (result.Success)
                return View(result.Data);

            return BadRequest("Error occured");
        }
    }
}
