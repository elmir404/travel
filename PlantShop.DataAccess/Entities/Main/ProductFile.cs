namespace PlantShop.DataAccess.Entities.Main
{
    public class ProductFile : File
    {
        public int Id { get; set; }
        public Product Product { get; set; }
    }
}
