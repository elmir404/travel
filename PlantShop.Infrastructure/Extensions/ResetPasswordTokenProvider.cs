using Microsoft.AspNetCore.Identity;
using PlantShop.DataAccess.Entities.Users;

namespace PlantShop.Infrastructure.Extensions
{
    public class ResetPasswordTokenProvider: TotpSecurityStampBasedTokenProvider<ApplicationUser>
    {
        public const string ProviderKey = "ResetPassword";

        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(false);
        }
    }
}
