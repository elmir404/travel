using Microsoft.AspNetCore.Identity;

namespace PlantShop.DataAccess.Entities.Users
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsVerify { get; set; }
        public string? Random { get; set; }
        
    }
}
