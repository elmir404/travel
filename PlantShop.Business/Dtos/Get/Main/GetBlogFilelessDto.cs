namespace PlantShop.Business.Dtos.Get.Main;
public class GetBlogFilelessDto
{
    public int Id { get; set; }
    public string AzTitle { get; set; }
    public string EnTitle { get; set; }
    public string RuTitle { get; set; }

    public string AzDescription { get; set; }
    public string EnDescription { get; set; }
    public string RuDescription { get; set; }
    public DateTime RegDate { get; set; }
}
