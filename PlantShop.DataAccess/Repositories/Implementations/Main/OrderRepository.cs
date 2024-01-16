using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;

namespace PlantShop.DataAccess.Repositories.Implementations.Main
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(PlantShopDbContext plantShopDbContext) : base(plantShopDbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetActiveOrders()
        {
            return await GetAsQueryable()
               
                .Include(x => x.ProductOrders)
                .Include(x => x.Number)
                .Where(x => x.isActive == true).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersByStatus(int status)
        {
            return await GetAsQueryable() 

                .Include(x => x.ProductOrders)
                 .Include(x => x.Number)
                .Where(x => x.isActive == true && x.Status==status && x.isConfirmed==true).ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await GetAsQueryable().Include(x => x.ProductOrders).Include(x=>x.Number).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Order> GetOrderByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await GetAsQueryable()

                 .Include(x => x.ProductOrders)
                  .Include(x => x.Number)
                 .Where(x => x.isActive == true).ToListAsync();
        }
    }
}
