using System.ComponentModel.DataAnnotations;

namespace PlantShop.Business.Dtos.Post.Users
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "istifadəçi adını daxil edin")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "şifrəni daxil edin")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
