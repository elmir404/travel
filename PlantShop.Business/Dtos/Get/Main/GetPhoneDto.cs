using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetPhoneDto
    {
        public int Id { get; set; }
        public string Phone { get; set; }
       
        public bool IsActive { get; set; }
    }
}
