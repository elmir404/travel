using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;

namespace PlantShop.DataAccess.Entities.Main
{
    public class File : EntityBase, IEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}
