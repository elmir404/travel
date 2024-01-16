using PlantShop.DataAccess.Abstractions;
using PlantShop.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.DataAccess.Entities.Main;

public class PhoneNumber : EntityBase, IEntity
{
    public string Phone { get; set; }
    public bool IsConfirmed { get; set; }
    public string Code { get; set; }
    public DateTime ActiveDate { get; set; } 
    public bool IsActive { get; set; }
    public ICollection<Order> Orders { get; set; }
    public IEnumerable<Contact> Contacts { get; set; }
}
