using PlantShop.Business.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetSearchDto
    {
        public SearchResult<IEnumerable<GetProductDto>>? Products { get; set; }
        public List<GetCategoryDto>? Categories { get; set; }
        public int? categoryId { get; set; } = 0;
    }
}
