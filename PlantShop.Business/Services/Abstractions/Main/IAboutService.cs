using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;

namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IAboutService
    {
        #region Get
        Task<ServiceResult> GetAbout();
        Task<ServiceResult> GetAboutForUpdate(int id);
        #endregion

        #region Post
        Task<ServiceResult> AddAbout(AddAboutDto addAboutDto);
        Task<ServiceResult> UpdateAbout(UpdateAboutDto updateAboutDto);
        #endregion
    }
}
