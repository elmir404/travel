

using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.DataAccess.Repositories.Abstractions.Main
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Category>> GetActiveCategories();
        Task<IEnumerable<Category>> GetDeactiveCategories();
        Task<Category> GetCategory(int categoryId);
        Task<Category> DeactiveCategory(int categoryId);
        Task<Category> ActiveCategory(int categoryId);
    }
}
