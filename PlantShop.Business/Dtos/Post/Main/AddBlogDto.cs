
using Microsoft.AspNetCore.Http;

namespace PlantShop.Business.Dtos.Post.Main;
public class AddBlogDto
{
    public string AzTitle { get; set; }
    public string EnTitle { get; set; }
    public string JpTitle { get; set; }
    public string ViTitle { get; set; }

    public string AzDescription { get; set; }
    public string EnDescription { get; set; }
    public string JpDescription { get; set; }
    public string ViDescription { get; set; }


    public IFormFileCollection Files { get; set; }
}
