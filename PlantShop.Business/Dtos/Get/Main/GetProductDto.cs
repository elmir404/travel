using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string AzName { get; set; }
        public string EnName { get; set; }
        public string RuName { get; set; }
        public string Slug { get; set; }
        public int? Inrst { get; set; }
        public string? AzDescription { get; set; }
        public string? EnDescription { get; set; }
        public string? RuDescription { get; set; }
        public double Price { get; set; }
        public double? UpPrice { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<GetFileDto> ProductFiles { get; set; }



    }
}
