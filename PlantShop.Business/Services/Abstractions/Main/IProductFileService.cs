using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;


namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IProductFileService
    {
        Task<ServiceResult> AddRangeAsync(AddProductDto product, int productId);
        Task<ServiceResult> UpdateRangeAsync(UpdateProductDto updateProductDto);
    }
}
