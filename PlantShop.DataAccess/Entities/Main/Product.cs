using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;

namespace PlantShop.DataAccess.Entities.Main
{
    public class Product : EntityBase, IEntity
    {
        public string AzName { get; set; }
        public string EnName { get; set; }
        public string RuName { get; set; }
        public string? AzDescription { get; set; }
        public string? EnDescription { get; set; }
        public string? RuDescription { get; set; }
        public string? Slug { get; set; }
        public double Price { get; set; }
        public double? UpPrice { get; set; } 
        public int? Inrst { get; set; } 
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductFile> ProductFiles { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
