using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.DataAccess.Repositories.Implementations.Main
{
    public class PhoneRepository : GenericRepository<PhoneNumber>, IPhoneRepository
    {
        public PhoneRepository(PlantShopDbContext plantShopDbContext) : base(plantShopDbContext)
        {
        }

        public async Task<IEnumerable<PhoneNumber>> GetActivePhones()
        {
            return await GetAsQueryable()
                .Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<IEnumerable<PhoneNumber>> GetAll()
        {
            return await GetAsQueryable().ToListAsync();
        }

        public async Task<PhoneNumber> GetPhone(int id)
        {
            return await GetAsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PhoneNumber> GetPhoneByNumber(string phone)
        {
            return await GetAsQueryable().Where(x=>x.IsActive==true).FirstOrDefaultAsync(x => x.Phone == phone);
        }

        
    }
}
