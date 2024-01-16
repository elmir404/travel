using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;
using PlantShop.DataAccess.Entities.Users;

namespace PlantShop.DataAccess.Entities.Main;

public class Order : EntityBase, IEntity
{
    public ICollection<ProductOrder> ProductOrders { get; set; }
    public int Count { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool isConfirmed { get; set; }
    public bool isActive { get; set; }
    public int? Status { get; set; }
    public string? Random { get; set; }
    public string? PromoCode { get; set; }
    public double TotalUpPrice { get; set; }
    public double TotalPrice { get; set; }
    public int Inrst { get; set; }
    public string? Message { get; set; }
    public int? numberId { get; set; }
    public PhoneNumber? Number { get; set; }
}
