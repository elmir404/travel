

using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;

namespace PlantShop.DataAccess.Entities.Main;
public class Blog : EntityBase, IEntity
{
    public string AzTitle { get; set; }
    public string EnTitle { get; set; }
    public string JpTitle { get; set; }
    public string ViTitle { get; set; }
    public string AzDescription { get; set; }
    public string EnDescription { get; set; }
    public string JpDescription { get; set; }
    public string ViDescription { get; set; }
    public string? Slug { get; set; }
    public bool IsActive { get; set; }
    public ICollection<BlogFile> BlogFiles { get; set; }
}
