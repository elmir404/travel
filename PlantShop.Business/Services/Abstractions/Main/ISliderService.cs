using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;

namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface ISliderService
    {
        Task<ServiceResult> AddSlider(AddSliderDto sliderDto);
        Task<ServiceResult> GetSlider(int id);
        Task<ServiceResult> GetSliderForUpdate(int id);
        Task<ServiceResult> GetSliders();
        Task<ServiceResult> UpdateSlider(UpdateSliderDto updateSliderDto);
        Task<ServiceResult> DeleteSlider(int id);
    }
}
