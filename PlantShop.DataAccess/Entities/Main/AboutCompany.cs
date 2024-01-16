using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;

namespace PlantShop.DataAccess.Entities.Main
{
    public class AboutCompany : EntityBase, IEntity
    {
        public string Address { get; set; }
        public string MapAddress { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? TwitterLink { get; set; }
    }
}
