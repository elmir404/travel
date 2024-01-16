using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Models;
using LinqKit;

namespace PlantShop.DataAccess.Repositories.Implementations.Main
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(PlantShopDbContext plantShopDbContext) : base(plantShopDbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetActiveProducts()
        {
            return await GetAsQueryable().Include(x => x.ProductFiles).Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetDeactiveProducts()
        {
            return await GetAsQueryable().Where(x => x.IsActive == false).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveProductsIdName()
        {
            return await GetAsQueryable().Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<bool> GetProductNameCondition(string name)
        {
            if (name != null)
            {
                var result = await GetAsQueryable().Where(x => x.AzName == name).ToListAsync();

                if (result.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await GetAsQueryable().Include(x => x.ProductFiles).ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await GetAsQueryable().Include(x => x.Category).Include(x => x.ProductFiles).FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryProducts(int categoryId)
        {
            return await GetAsQueryable().Include(x => x.ProductFiles).Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public IQueryable<Product> SearchProduct(HomeSearchModel searchModel)
        {
            var query = GetAsQueryable()
               .Include(x => x.ProductFiles)
               .Where(x => x.IsActive == true);
            if (searchModel.categoryIds != null && searchModel.categoryIds.Any())
            {
                var productPredicate = PredicateBuilder.False<Product>();


                foreach (var categoryId in searchModel.categoryIds)
                {
                    productPredicate = productPredicate.Or(x => x.Category.Id == categoryId);
                }
                query = query.Where(productPredicate);
            }
            if (searchModel.ProductName != null && searchModel.ProductName.Any())
            {
                var productPredicate = PredicateBuilder.False<Product>();

                productPredicate = productPredicate.Or(x => x.AzName == searchModel.ProductName
                     || x.EnName == searchModel.ProductName
                     || x.RuName == searchModel.ProductName);

                query = query.Where(productPredicate);
            }
            return query.OrderBy(x => x.Id);
        }

        public async Task<Product> GetProductBySlug(string slug)
        {
            return await GetAsQueryable().Include(x => x.Category).Include(x => x.ProductFiles).FirstOrDefaultAsync(x => x.Slug == slug);
        }
    }
}
