using AutoMapper;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Abstractions.Storage;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.UnitOfWorks;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class SliderFileService : ISliderFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorage _storage;
        private readonly IMapper _mapper;

        public SliderFileService(IUnitOfWork unitOfWork, IStorage storage, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storage = storage;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddRangeAsync(AddSliderDto sliderDto, int sliderId)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storage.UploadAsync("photo-slider", sliderDto.Files);
            var sliders = _unitOfWork.Repository<Slider>().Get(x => x.Id == sliderId);
            _unitOfWork.Repository<SliderFile>().AddRange(result.Select(x => new SliderFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Slider = sliders
            }));
            _unitOfWork.Commit();
            return new ServiceResult(true);
        }

        public async Task<ServiceResult> UpdateRangeAsync(UpdateSliderDto updateSliderDto)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storage.UploadAsync("photo-slider", updateSliderDto.Files);
            var sliders = _unitOfWork.Repository<Slider>().Get(x => x.Id == updateSliderDto.Id);
            var entity = await _unitOfWork.Repository<SliderFile>().GetAllAsync(x => x.Slider.Id == updateSliderDto.Id);
            _unitOfWork.Repository<SliderFile>().DeleteRange(entity);
            _unitOfWork.Repository<SliderFile>().UpdateRange(result.Select(x => new SliderFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Slider = sliders
            }));
            _unitOfWork.Commit();
            return new ServiceResult(true);
        }
    }
}
