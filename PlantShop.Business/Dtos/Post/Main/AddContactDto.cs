using System.ComponentModel.DataAnnotations;

namespace PlantShop.Business.Dtos.Post.Main
{
    public class AddContactDto
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Subject { get; set; }
        public string? Code { get; set; }
        public string Message { get; set; }
    }
}
