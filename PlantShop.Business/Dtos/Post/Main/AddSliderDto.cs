using Microsoft.AspNetCore.Http;
using PlantShop.Business.Dtos.Get.Main;

namespace PlantShop.Business.Dtos.Post.Main
{
    public class AddSliderDto
    {
        public string AzTitle { get; set; }
        public string? EnTitle { get; set; }
        public string? RuTitle { get; set; }
        public string AzDescription { get; set; }
        public string? EnDescription { get; set; }
        public string? RuDescription { get; set; }
        public bool IsActive { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
