using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlantShop.Business.Dtos.Get.Users;
using PlantShop.Business.Dtos.Post.Users;
using PlantShop.Business.Helpers;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Abstractions.Users;
using PlantShop.Business.Services.Implementations.Main;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Users;
using PlantShop.DataAccess.Models;
using PlantShop.DataAccess.UnitOfWorks;
using PlantShop.Infrastructure.Extensions;
using System.Security.Claims;


namespace PlantShop.Business.Services.Implementations.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly PlantShopDbContext _dbContext;
        private readonly ISession _session;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, PlantShopDbContext dbContext,
            RoleManager<ApplicationRole> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _dbContext = dbContext;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        public async Task<ServiceResult> AddUser(RegisterUserDto registerUser)
        {
            var requestData = _mapper.Map<ApplicationUser>(registerUser);
            IdentityResult result = await _userManager.CreateAsync(requestData, "1234dd");

            if (result.Succeeded)
                return new ServiceResult(true, "registration completed");

            return new ServiceResult(false, String.Join(';', result.Errors.Select(x => x.Code)));
        }


        public async Task<ServiceResult> GetLoggedUser()
        {
            try
            {
                var userData = _session.GetObject<UserSessionDto>("userdetail");
                if (userData == null)
                    return new ServiceResult(false, "data not found");

                return new ServiceResult(true, userData);
            }
            catch (Exception)
            {
                return new ServiceResult(false, "data not found");
            }
        }

        public async Task<ServiceResult> GetUser(string userId)
        {
            try
            {
                var result = await _userManager.FindByIdAsync(userId);

                if (result != null)
                {
                    var response = _mapper.Map<ApplicationUserDto>(result);
                    return new ServiceResult(true, response, "user successfully found");
                }

                return new ServiceResult(false, "user not found");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetUsers()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                if (users != null)
                {
                    var response = _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
                    return new ServiceResult(true, response);
                }

                return new ServiceResult(false, "user not found");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> LogIn(LoginUserDto loginUser)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginUser.UserName);

                if (user == null)
                    return new ServiceResult(false, "user not found");


                var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, true, false);

                if (!result.Succeeded)
                {

                    return new ServiceResult(false, "loginfiled");
                }

                var detail = new UserSessionDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.Name,
                    LastName = user.Surname,
                    Email = user.Email,
                };
                _session.SetObject("userdetail", detail);
                return new ServiceResult(true, "login success");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return new ServiceResult(true, "successfully logout");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> Register(RegisterUserDto registerUser)
        {
            try
            {

                var name = await _userManager.FindByNameAsync(registerUser.Phone);
                if (name != null)
                    return new ServiceResult(false, "username alreadyused");
                ApplicationUser requestData = new ApplicationUser
                {
                    Name = registerUser.Name,
                    Surname = registerUser.Surname,
                    UserName = registerUser.Phone,
                    Email = registerUser.Email,
                    IsVerify = false,

                };
                IdentityResult result = await _userManager.CreateAsync(requestData, registerUser.Password);

                if (result.Succeeded)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(registerUser.Phone);
                    var res = await _userManager.AddToRoleAsync(user, "");
                    //await _mailService.SendMailAsync(registerUser.Email, "User Information", $"Salam, {requestData.Name},</br>Hesabınız yaradıldı! </br> Daxil Olmaq üçün məlumatlarınız!</br>Telefon Nömrəsi:{registerUser.Phone}</br> Email:{registerUser.Email}</br> Şifrə:{registerUser.Password}</br>");
                    return new ServiceResult(true, "registration completed");
                }
                return new ServiceResult(false, "unknownerror");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return new ServiceResult(true);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> Verify(string phone, string inputRandom)
        {
            var name = await _userManager.FindByNameAsync(phone);
            if (name == null)
                return new ServiceResult(false, "user not found");
            if (inputRandom == null)
            {
                return new ServiceResult(false, "rantom not found");
            }
            else if (inputRandom.ToLower() != name.Random.ToLower())
            {
                return new ServiceResult(false, "random not match");
            }
            name.IsVerify = true;
            await _userManager.UpdateAsync(name);
            return new ServiceResult(true, "phone verified");
        }

        public async Task<ServiceResult> PasswordResetAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                resetToken = resetToken.UrlEncode();
                await _mailService.SendPasswordResetMail(email, user.UserName, resetToken);
                return new ServiceResult(true, "tokenwassend");
            }
            else
            {
                return new ServiceResult(false, "emailnotfound");
            }
        }

        public Task<bool> VerifyResetTokenAsync(string resetToke, string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePassword(string userName, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Task<bool> EmailConfirmAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyConfirmAsync(string userName, string confirmToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> AddRole(AddRoleModel model)
        {
            ApplicationRole role = new();
            role.Name = model.Name;
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ServiceResult(true);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> GetRole()
        {
            try
            {
                var result = await _roleManager.Roles.ToListAsync();

                if (result != null)
                {
                    var response = _mapper.Map<IEnumerable<ApplicationRoleDto>>(result);
                    return new ServiceResult(true, response);
                }

                return new ServiceResult(false, "user not found");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public Task<ServiceResult> AddClaim(AddClaimDto claim)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResult> AddUserRole(int userId, AddClaimDto model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());


                if (user == null)
                    return new ServiceResult(false, "user not found");

                var result = await _userManager.AddToRoleAsync(user, model.RoleName);

                if (result.Succeeded)
                    return new ServiceResult(true, "aded role successfull");

                return new ServiceResult(false, String.Join(';', result.Errors.Select(x => x.Code)));
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }
    }
}
