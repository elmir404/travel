using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;

namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IProductService
    {
        #region Get
        Task<ServiceResult> GetProduct(int id);
        Task<ServiceResult> GetProductBySlug(string slug);
        Task<ServiceResult> GetProductForUpdate(int id);
        Task<ServiceResult> GetProducts();
        Task<ServiceResult> GetDeactiveProducts();
        Task<ServiceResult> SearchProduct(int currentPage, int pageSize,SearchModelDto searchModelDto);
        Task<ServiceResult> GetActiveProducts();
        #endregion

        #region Post
        Task<ServiceResult> AddProduct(AddProductDto product);
        Task<ServiceResult> UpdateProduct(UpdateProductDto updateProductDto);
        Task<ServiceResult> DeleteProduct(int id);
        Task<ServiceResult> DeactiveProduct(int id);
        Task<ServiceResult> ActiveProduct(int id);
        #endregion
    }
}
