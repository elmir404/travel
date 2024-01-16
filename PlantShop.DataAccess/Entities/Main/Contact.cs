using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace PlantShop.DataAccess.Entities.Main
{
    public class Contact : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Subject { get; set; } 
        public string Message { get; set; }
        public int? numberId { get; set; }
        public PhoneNumber? Number { get; set; } 
    }
}
