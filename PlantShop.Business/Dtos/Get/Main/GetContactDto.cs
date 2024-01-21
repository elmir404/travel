using System.ComponentModel.DataAnnotations;

namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetContactDto
    {
        public int Id { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string Email { get; set; }
		public string Message { get; set; }
	}
}
