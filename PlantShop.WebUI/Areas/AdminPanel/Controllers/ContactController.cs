using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Services.Abstractions.Main;

namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

      
        public async Task<IActionResult> List()
        {
            var response = await _contactService.GetContacts();
            return View(response.Data);
        }
        public async Task<IActionResult> Detail(int messageId)
        {
            var result = await _contactService.GetContact(messageId);
            return View(result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int messageId)
        {
            var result = await _contactService.DeleteContactById(messageId);

            return Ok(result);
        }
    }
}
