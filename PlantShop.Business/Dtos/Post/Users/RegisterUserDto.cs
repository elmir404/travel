using System.ComponentModel.DataAnnotations;

namespace PlantShop.Business.Dtos.Post.Users
{
    public class RegisterUserDto
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [Required]
        
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Şifrələr eyni deyil")]
        public string ConfirmPassword { get; set; }
    }
}
