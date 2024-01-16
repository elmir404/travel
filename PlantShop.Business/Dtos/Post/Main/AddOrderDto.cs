
namespace PlantShop.Business.Dtos.Post.Main
{
    public class AddOrderDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string? Message { get; set; }
        public List<AddOrderProductDto>? OrderProducts { get; set; }
    }

 
}
