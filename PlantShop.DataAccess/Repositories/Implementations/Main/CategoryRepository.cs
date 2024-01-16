using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using System.Security.Policy;

namespace   PlantShop.DataAccess.Repositories.Implementations.Main
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PlantShopDbContext plantShopDbContext) : base(plantShopDbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetActiveCategories()
        {
            return await GetAsQueryable()
                .Include(x => x.Products.Where(x => x.IsActive == true))
                .ThenInclude(x => x.ProductFiles)
                .Where(x => x.IsActive == true).ToListAsync();
            
        }

        public async Task<IEnumerable<Category>> GetDeactiveCategories()
        {
            return await GetAsQueryable().Where(x => x.IsActive == false).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await GetAsQueryable().ToListAsync();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await GetAsQueryable().FirstOrDefaultAsync(x => x.Id == categoryId);
        }
        public async Task<Category> DeactiveCategory(int categoryId)
        {
            return await GetAsQueryable().Include(x => x.Products.Where(y => y.IsActive == true)).FirstOrDefaultAsync(x => x.Id == categoryId);
        }
        public async Task<Category> ActiveCategory(int categoryId)
        {
            return await GetAsQueryable().Include(x => x.Products.Where(y => y.IsActive == false)).FirstOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
