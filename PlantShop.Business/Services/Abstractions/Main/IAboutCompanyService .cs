using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;

namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IAboutCompanyService
    {
        #region Get
        Task<ServiceResult> GetAboutCompany();
        Task<ServiceResult> GetAboutCompany(int id);
        Task<ServiceResult> GetAboutCompanyForUpdate(int id);
        #endregion

        #region Post
        Task<ServiceResult> AddAboutCompany(AddAboutCompany addAboutCompany);
        Task<ServiceResult> UpdateAboutCompany(UpdateAboutCompany updateAboutCompany);
        #endregion
    }
}
