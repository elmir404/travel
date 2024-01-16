using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Models;

namespace PlantShop.DataAccess.Repositories.Abstractions.Main
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProduct(int id);
        Task<Product> GetProductBySlug(string slug);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetActiveProducts();
        Task<IEnumerable<Product>> GetDeactiveProducts();
        Task<IEnumerable<Product>> GetActiveProductsIdName();
        Task<bool> GetProductNameCondition(string name);
        Task<IEnumerable<Product>> GetProductByCategoryProducts(int categoryId);
        IQueryable<Product> SearchProduct(HomeSearchModel searchModel);
    }
}
