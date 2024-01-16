using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;

namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IOrderService
    {
        #region Get
        Task<ServiceResult> GetOrders();
        Task<ServiceResult> GetActiveOrders();
        Task<ServiceResult> GetActiveOrdersByStatus(int status);
        Task<ServiceResult> GetOder(int id);
        Task<ServiceResult> GetOderByPhone(string phone);
        #endregion

        #region Post
        Task<ServiceResult> AddOrder(AddOrderDto addOrderDto);
        Task<ServiceResult> DeactiveOrder(int id);
        Task<ServiceResult> changeStatus(int id,int status);
        Task<ServiceResult> OrderConfirmation(int id, string code);

        #endregion
    }
}
