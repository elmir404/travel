using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface ICategoryService
    {
        #region Get
        Task<ServiceResult> GetCategories();
        Task<ServiceResult> GetActiveCategories();
        Task<ServiceResult> GetDeactiveCategories();
        Task<ServiceResult> GetCategory(int id);
        #endregion

        #region Post
        Task<ServiceResult> AddCategory(AddCategoryDto category);
        Task<ServiceResult> UpdateCategory(AddCategoryDto categoryDto, int id);
        Task<ServiceResult> DeactiveCategory(int id);
        Task<ServiceResult> ActiveCategory(int id);
        #endregion

    }
}
