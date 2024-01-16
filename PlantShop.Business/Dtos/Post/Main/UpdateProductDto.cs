using Microsoft.AspNetCore.Http;

namespace PlantShop.Business.Dtos.Post.Main
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string AzName { get; set; }
        public string EnName { get; set; }
        public string RuName { get; set; }
        public string AzDescription { get; set; }
        public string EnDescription { get; set; }
        public string RuDescription { get; set; }
        public double Price { get; set; }
        public double UpPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
