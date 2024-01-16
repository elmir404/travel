using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;

namespace PlantShop.DataAccess.Entities.Main
{
    public class Category : EntityBase, IEntity
    {
        public string AzName { get; set; }
        public string EnName { get; set; }
        public string RuName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
