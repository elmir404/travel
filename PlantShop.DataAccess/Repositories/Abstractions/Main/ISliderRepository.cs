using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.DataAccess.Repositories.Abstractions.Main
{
    public interface ISliderRepository : IGenericRepository<Slider>
    {
        Task<IEnumerable<Slider>> GetAllSliders();
        Task<Slider> GetSlider(int id);
        Task<Slider> GetLast();
    }
}
