using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Implementations.Main;

namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderFileService _sliderFileService;

        public SliderController(ISliderService sliderService, ISliderFileService sliderFileService)
        {
            _sliderService = sliderService;
            _sliderFileService = sliderFileService;
        }

        public async Task<IActionResult> List()
        {
            var result = await _sliderService.GetSliders();
            return View(result.Data);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSliderDto addSliderDto)
        {
            if (!ModelState.IsValid)
            {

                foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }
            var result = await _sliderService.AddSlider(addSliderDto);
            if (result.Success)
            {
                addSliderDto.Files = Request.Form.Files;
                await _sliderFileService.AddRangeAsync(addSliderDto, (int)result.Data);
                return Redirect("/AdminPanel/Slider/List");
            }

            //ModelState.AddModelError("", "Butun hisselerin tam doldurulduqundan emin olun!");
            return View(result.Data);
        }

        public async Task<IActionResult> Update(int id)
        {
            var slider = await _sliderService.GetSliderForUpdate(id);
            return View(slider.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSliderDto updateSliderDto)
        {
            var result = await _sliderService.UpdateSlider(updateSliderDto);
            if (result.Success && updateSliderDto.Files != null)
            {
                updateSliderDto.Files = Request.Form.Files;
                await _sliderFileService.UpdateRangeAsync(updateSliderDto);
                return Redirect("/AdminPanel/Slider/List");
            }
            return Redirect("/AdminPanel/Slider/List");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _sliderService.DeleteSlider(Id);
            return Ok(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _sliderService.GetSlider(id);
            if (result.Success)
                return View(result.Data);

            return BadRequest("Error occured");
        }
    }
}
