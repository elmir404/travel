using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.DataAccess.Repositories.Abstractions.Main
{
    public interface IAboutRepository : IGenericRepository<About>
    {
        Task<About> GetAbout();
        Task<About> GetAbout(int aboutId);
    }
}
