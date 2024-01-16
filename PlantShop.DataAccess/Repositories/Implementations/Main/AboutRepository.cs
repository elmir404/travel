using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;

namespace PlantShop.DataAccess.Repositories.Implementations.Main
{
    public class AboutRepository : GenericRepository<About>, IAboutRepository
    {
        public AboutRepository(PlantShopDbContext plantShopDbContext) : base(plantShopDbContext)
        {
        }

        public async Task<About> GetAbout(int aboutId)
        {
            return await GetAsQueryable().FirstOrDefaultAsync(x => x.Id == aboutId);
        }

        public async Task<About> GetAbout()
        {
            return await GetAsQueryable().FirstOrDefaultAsync();
        }
    }
}
