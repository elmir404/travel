using PlantShop.Business.Dtos.Post.Users;
using PlantShop.Business.Services.Abstractions.Users;
using PlantShop.DataAccess.Entities.Users;
using PlantShop.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PlantShop.WebUI.Areas.Admin.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserController(IUserService userService, RoleManager<ApplicationRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userService.GetUsers();
            return View(users.Data);
        }

        public async Task<IActionResult> UserListGet()
        {

            return Ok();

        }
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterUserDto model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            var result = await _userService.Register(model);
            if (result.Message == "user not verified")
            {
                ModelState.AddModelError("", "his account hasn't been verified");
                return View(model);
            }
            return Redirect("/AdminPanel/User/UserList");
        }

        public async Task<IActionResult> Update(int Id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, AddClaimDto model)
        {
            var result = await _userService.AddUserRole(Id, model);
            if (result.Message == "user not found")
            {
                ModelState.AddModelError("", "his account hasn't been verified");
                return View(model);
            }
            return Redirect("/AdminPanel/User/UserList");
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int userId)
        {
            var result = await _userService.DeleteUser(userId.ToString());
            return Ok(result);
        }

        public async Task<IActionResult> RoleList()
        {
            var roles = await _userService.GetRole();
            return View(roles.Data);
        }

        public async Task<IActionResult> RoleListGet()
        {
            var roles = await _userService.GetRole();
            return Ok(roles);
        }

        public async Task<IActionResult> RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(AddRoleModel model)
        {
            if (!ModelState.IsValid)
            {

                foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();

            }

            var result = await _userService.AddRole(model);
            if (result.Success)
                return Redirect("/AdminPanel/User/RoleList");

            return BadRequest("Error occured");
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
    }
}
