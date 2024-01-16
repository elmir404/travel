using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Services.Abstractions.Main;

namespace PlantShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IAboutCompanyService _aboutCompanyService;
        private readonly IContactService _contactService;
        private readonly IPhoneService _phoneService;

        public ContactController(IAboutCompanyService aboutCompanyService, IContactService contactService, IPhoneService phoneService)
        {
            _aboutCompanyService = aboutCompanyService;
            _contactService = contactService;
            _phoneService = phoneService;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPhone(string phone)
        {
            var result = await _phoneService.AddPhone(phone);
            return Ok(result);
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
