using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.DataAccess.Repositories.Abstractions.Main
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
     
        Task<Order> GetOrder(int id);
        Task<Order> GetOrderByPhone(string phone);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetActiveOrders();
        Task<IEnumerable<Order>> GetActiveOrdersByStatus(int status);
    }
}
