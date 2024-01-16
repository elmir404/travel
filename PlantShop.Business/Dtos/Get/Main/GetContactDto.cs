using System.ComponentModel.DataAnnotations;

namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GetPhoneDto Number { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
