using PlantShop.Business.Dtos.Post.Users;
using PlantShop.Business.Services.Abstractions.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace PlantShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(IUserService userService, IStringLocalizer<AccountController> localizer)
        {
            _userService = userService;
            _localizer = localizer;

        }
        [HttpPost]
        public async Task<IActionResult> LogOut(string url)
        {
            var result = await _userService.LogOut();

            return RedirectToAction("Index", "Home");
            if (result.Success)
            {
                return LocalRedirect(url);
            }
            return View();
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return View("Error");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUser)
        {
            var result = await _userService.LogIn(loginUser);
            if (result.Message == "user not verified")
            {
                ModelState.AddModelError("", _localizer["This account hasn't been verified"]);
                return View(loginUser);
            }
            if (result.Message == "user not found")
            {
                ModelState.AddModelError("", _localizer["Email or Password is incorrect"]);

                return View(loginUser);

            }
            if (result.Message == "loginfiled")
            {
                ModelState.AddModelError("", _localizer["Email or Password is incorrect"]);
                return View(loginUser);
            }
            return Redirect("/AdminPanel/Contact/List");

        }

    }
}
