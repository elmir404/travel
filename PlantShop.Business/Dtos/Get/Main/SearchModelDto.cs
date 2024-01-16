namespace PlantShop.Business.Dtos.Get.Main
{
    public class SearchModelDto
    {
        public string ProductName { get; set; }
        public List<int> categoryIds { get; set; }
        public List<int> brandIds { get; set; }
    }
}
