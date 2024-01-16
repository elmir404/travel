using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public GetPhoneDto Number { get; set; }
        public string? Message { get; set; }
        public string Random { get; set; }
        public string PromoCode { get; set; }
        public DateTime RegDate { get; set; }
        public double TotalUpPrice { get; set; }
        public double TotalPrice { get; set; }
        public int Inrst { get; set; }
        public bool isConfirmed { get; set; }
        public bool isActive { get; set; }
        public int Status { get; set; }
        public int Count { get; set; }
        public ICollection<GetProductOrder> ProductOrders { get; set; }

    }
}
