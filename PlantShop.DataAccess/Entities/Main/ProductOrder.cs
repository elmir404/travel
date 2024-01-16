namespace PlantShop.DataAccess.Entities.Main
{
    public class ProductOrder
    {
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
        public double Price { get; set; }
        public double UpPrice { get; set; }
        public int Inrst { get; set; }
        public int Count { get; set; }
    }
}
