using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Services.Abstractions.Main;
using System.Data;

namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
	{
        private readonly IBlogService _blogService;
        private readonly IBlogFileService _blogFileService;

        public BlogController(IBlogService blogService, IBlogFileService blogFileService)
        {
            _blogService = blogService;
            _blogFileService = blogFileService;
        }




        public async Task<IActionResult> List()
        {
            var result = await _blogService.GetActiveBlogs();
            return View(result.Data);
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogDto model)
        {
         
            if (!ModelState.IsValid)
            {

                foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();

            }
            var result = await _blogService.AddBlog(model);
            if (result.Success)
            {
                model.Files = Request.Form.Files;
                await _blogFileService.AddRangeAsync(model, (int)result.Data);
                return Redirect("/AdminPanel/Blog/List");
            }
            ModelState.AddModelError("", "Bu adda məhsul var");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            
            var product = await _blogService.GetBlog(id);

            return View(product.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,AddBlogDto updateProductDto)
        {
         
            var result = await _blogService.UpdateBlog(updateProductDto,id);
            if (result.Success && updateProductDto.Files != null)
            {
                updateProductDto.Files = Request.Form.Files;
                await _blogFileService.UpdateRangeAsync(updateProductDto,id);
                return Redirect("/AdminPanel/Blog/List");
            }

            return Redirect("/AdminPanel/Product/List");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _blogService.DeleteBlogById(id);
            return Ok(result);
        }

        //public async Task<IActionResult> Deactive()
        //{
        //    var result = await _blogService.get();
        //    if (result.Success)
        //        return View(result.Data);

        //    return BadRequest();
        //}

        //[HttpPatch]
        //public async Task<IActionResult> Deactive(int id)
        //{
        //    var result = await _productService.DeactiveProduct(id);
        //    if (result.Success)
        //        return Ok(result);

        //    return BadRequest("Error occured");
        //}

        //[HttpPatch]
        //public async Task<IActionResult> Active(int id)
        //{
        //    var result = await _productService.ActiveProduct(id);
        //    if (result.Success)
        //        return Ok(result);

        //    return BadRequest("Error occured");
        //}

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _blogService.GetBlog(id);
            if (result.Success)
                return View(result.Data);

            return BadRequest("Error occured");
        }
    }
}
