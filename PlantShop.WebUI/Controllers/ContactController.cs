using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Services.Abstractions.Main;

namespace PlantShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IAboutCompanyService _aboutCompanyService;
        private readonly IContactService _contactService;
        private readonly IStringLocalizer<ContactController> _localizer;
        public ContactController(IAboutCompanyService aboutCompanyService, IContactService contactService, IStringLocalizer<ContactController> localizer)
        {
            _aboutCompanyService = aboutCompanyService;
            _contactService = contactService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();
        }
       

        [HttpPost]
        public async Task<IActionResult> Index(AddContactDto model)
        {
            if (!ModelState.IsValid)
            {
                
                foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }
            var response = await _contactService.AddContact(model);
            if (response.Success == true)
            {
                ModelState.AddModelError("", "Mesajınız uğurla göndərilmişdir!");
                
                return View();
            }
            return RedirectToAction();
        }
    }
}
