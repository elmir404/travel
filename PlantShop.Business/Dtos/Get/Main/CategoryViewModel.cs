namespace PlantShop.Business.Dtos.Get.Main
{
    public class CategoryViewModel
    {
        public IEnumerable<GetCategoryDto> Categories { get; set; }
        public GetProductDto Product { get; set; }
    }
}
