namespace Letter.Business.Dtos.Get.Main;
public class GetBlogDto
{
    public int Id { get; set; }
    public string AzTitle { get; set; }
    public string EnTitle { get; set; }
    public string RuTitle { get; set; }

    public string AzDescription { get; set; }
    public string EnDescription { get; set; }
    public string RuDescription { get; set; }
    public DateTime RegDate { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<GetBlogFileDto> BlogFiles { get; set; }
}
