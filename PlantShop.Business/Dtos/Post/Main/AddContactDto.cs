using System.ComponentModel.DataAnnotations;

namespace PlantShop.Business.Dtos.Post.Main
{
    public class AddContactDto
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
