using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;

namespace PlantShop.DataAccess.Repositories.Implementations.Main
{
    public class SliderRepository : GenericRepository<Slider>, ISliderRepository
    {
        public SliderRepository(PlantShopDbContext plantShopDbContext) : base(plantShopDbContext)
        {
        }

        public async Task<IEnumerable<Slider>> GetAllSliders()
        {
            return await GetAsQueryable().Include(x => x.SliderFiles).ToListAsync();
        }

        public async Task<Slider> GetSlider(int id)
        {
            return await GetAsQueryable().Include(x => x.SliderFiles).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
