using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Dtos.Get.Main;


namespace PlantShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        

        public HomeController()
        {
           
        }

        public async Task<IActionResult> Index()
        {
           
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAboutCompany()
        {
            
            return Ok();
        }
        public IActionResult SetLanguage(string culture, string url)
        {
            try
            {
                Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                return LocalRedirect(url);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
                throw;
            }

        }
    }
}
