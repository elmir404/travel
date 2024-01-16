using PlantShop.DataAccess.Entities.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetProductOrder
    {
        public GetProductDto? Product { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OrderId { get; set; }
        public double Price { get; set; }
        public double UpPrice { get; set; }
        public int Inrst { get; set; }
        public int Count { get; set; }
    }
}
