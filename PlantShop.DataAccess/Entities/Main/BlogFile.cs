namespace PlantShop.DataAccess.Entities.Main;
public class BlogFile : File
{
    public int Id { get; set; }
    public Blog Blog { get; set; }
}
