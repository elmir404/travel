using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;

namespace PlantShop.DataAccess.Repositories.Implementations.Main
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(PlantShopDbContext plantShopDbContext) : base(plantShopDbContext)
        {
        }

        public async Task<Contact> GetContact(int contactId)
        {
            return await GetAsQueryable().FirstOrDefaultAsync(x => x.Id == contactId);
        }
    }
}
