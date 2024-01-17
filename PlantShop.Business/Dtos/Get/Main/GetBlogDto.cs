namespace PlantShop.Business.Dtos.Get.Main;
public class GetBlogDto
{
    public int Id { get; set; }
    public string AzTitle { get; set; }
    public string EnTitle { get; set; }
    public string JpTitle { get; set; }
    public string ViTitle { get; set; }

    public string AzDescription { get; set; }
    public string EnDescription { get; set; }
    public string JpDescription { get; set; }
    public string ViDescription { get; set; }
    public string Slug { get; set; }
    public DateTime RegDate { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<GetBlogFileDto> BlogFiles { get; set; }
}
