using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;

namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface ISliderFileService
    {
        Task<ServiceResult> AddRangeAsync(AddSliderDto sliderDto, int sliderId);
        Task<ServiceResult> UpdateRangeAsync(UpdateSliderDto updateSliderDto);
    }
}
