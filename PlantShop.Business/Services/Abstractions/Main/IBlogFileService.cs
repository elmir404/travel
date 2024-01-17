
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;


namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IBlogFileService
    {
        Task<ServiceResult> AddRangeAsync(AddBlogDto blog, int blogId);
        Task<ServiceResult> UpdateRangeAsync(AddBlogDto blog, int blogId);
    }
}
