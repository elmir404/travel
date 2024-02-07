using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;

namespace PlantShop.DataAccess.Entities.Main
{
    public class Slider : EntityBase, IEntity
    {
        public string AzTitle { get; set; }
        public string? EnTitle { get; set; }
        public string? RuTitle { get; set; }
        public string? JpTitle { get; set; }
        public string? ViTitle { get; set; }
        public string AzDescription { get; set; }
        public string? EnDescription { get; set; }
        public string? RuDescription { get; set; }
        public string? JpDescription { get; set; }
        public string? ViDescription { get; set; }
        public bool IsActive { get; set; }
        public ICollection<SliderFile> SliderFiles { get; set; }
    }
}
