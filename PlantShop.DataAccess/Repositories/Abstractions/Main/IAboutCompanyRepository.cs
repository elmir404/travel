using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.DataAccess.Repositories.Abstractions.Main
{
    public interface IAboutCompanyRepository : IGenericRepository<AboutCompany>
    {
        Task<AboutCompany> GetAboutCompany();
        Task<AboutCompany> GetAboutCompany(int aboutId);
    }
}
