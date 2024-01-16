using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;

namespace PlantShop.DataAccess.Repositories.Implementations.Main
{
    public class AboutCompanyRepository : GenericRepository<AboutCompany>, IAboutCompanyRepository
    {
        public AboutCompanyRepository(PlantShopDbContext plantShopDbContext) : base(plantShopDbContext)
        {
        }

        public async Task<AboutCompany> GetAboutCompany(int aboutId)
        {
            return await GetAsQueryable().FirstOrDefaultAsync(x => x.Id == aboutId);
        }

        public async Task<AboutCompany> GetAboutCompany()
        {
            return await GetAsQueryable().FirstOrDefaultAsync();
        }
    }
}
