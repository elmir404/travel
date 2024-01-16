namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string AzName { get; set; }
        public string EnName { get; set; }
        public string RuName { get; set; }
        public bool IsActive { get; set; }
        public List<GetProductDto>? Products { get; set; }
    }
}
