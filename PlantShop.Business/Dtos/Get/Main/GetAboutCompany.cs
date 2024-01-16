namespace PlantShop.Business.Dtos.Get.Main
{
    public class GetAboutCompany
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string MapAddress { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? TwitterLink { get; set; }
    }
}
