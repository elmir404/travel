using AutoMapper;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using PlantShop.DataAccess.Repositories.Implementations.Main;
using PlantShop.DataAccess.UnitOfWorks;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAboutRepository _aboutRepository;
        public AboutService(IMapper mapper, IUnitOfWork unitOfWork, IAboutRepository aboutRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _aboutRepository = aboutRepository;
        }
        #region Get
        public async Task<ServiceResult> GetAbout()
        {
            var abouts = await _aboutRepository.GetAbout();
            if (abouts != null)
            {
                var response = _mapper.Map<GetAboutDto>(abouts);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> GetAboutForUpdate(int id)
        {
            try
            {
                var about = await _aboutRepository.GetAbout(id);
                if (about != null)
                {
                    var response = _mapper.Map<UpdateAboutDto>(about);
                    return new ServiceResult(true, response);
                }
                return new ServiceResult(false, "data not found");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        #endregion

        #region Post
        public async Task<ServiceResult> AddAbout(AddAboutDto addAboutDto)
        {
            var request = _mapper.Map<About>(addAboutDto);
            await _unitOfWork.Repository<About>().AddAsync(request);
            _unitOfWork.Commit();
            var response = _mapper.Map<GetAboutDto>(request);
            return new ServiceResult(true, response.Id);
        }
        public async Task<ServiceResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var about = await _aboutRepository.GetAbout(updateAboutDto.Id);
            if (about != null)
            {
                if (updateAboutDto.AzTitle != null)
                {
                    about.AzTitle = updateAboutDto.AzTitle;
                }
                
                if (updateAboutDto.AzDescription != null)
                {
                    about.AzDescription = updateAboutDto.AzDescription;
                }

                _unitOfWork.Repository<About>().Update(about);
                _unitOfWork.Commit();
                var response = _mapper.Map<GetAboutDto>(about);
                return new ServiceResult(true, response.Id);
            }
            return new ServiceResult(false);
        }

        #endregion
    }
}
