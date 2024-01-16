using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using PlantShop.DataAccess.Repositories.Implementations.Main;
using PlantShop.DataAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.Business.Services.Implementations.Main
{
    internal class SliderService : ISliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #region Get

        public async Task<ServiceResult> GetSlider(int id)
        {
            try
            {
                var result = await _sliderRepository.GetSlider(id);
                if (result == null)
                    return new ServiceResult(false, "data not found");

                var response = _mapper.Map<GetSliderDto>(result);
                return new ServiceResult(true, response);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetSliderForUpdate(int id)
        {
            try
            {
                var result = await _sliderRepository.GetSlider(id);
                if (result == null)
                    return new ServiceResult(false, "data not found");

                var response = _mapper.Map<UpdateSliderDto>(result);
                return new ServiceResult(true, response);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetSliders()
        {
            var slider = await _sliderRepository.GetAllSliders();
            if (slider != null)
            {
                var response = _mapper.Map<ICollection<GetSliderDto>>(slider);
                return new ServiceResult(true, response);
            }

            return new ServiceResult(false);
        }

        #endregion

        #region Post

        public async Task<ServiceResult> AddSlider(AddSliderDto sliderDto)
        {
            var request = _mapper.Map<Slider>(sliderDto);
            request.IsActive = true;
            await _unitOfWork.Repository<Slider>().AddAsync(request);
            _unitOfWork.Commit();
            var response = _mapper.Map<GetSliderDto>(request);
            return new ServiceResult(true, response.Id);
        }
        
        public async Task<ServiceResult> DeleteSlider(int id)
        {
            var slider = await _sliderRepository.GetSlider(id);
            if (slider != null)
            {
                slider.IsActive = false;
                _unitOfWork.Repository<Slider>().Delete(slider);
                _unitOfWork.Commit();
                return new ServiceResult(true);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> UpdateSlider(UpdateSliderDto updateSliderDto)
        {
            var slider = await _sliderRepository.GetSlider(updateSliderDto.Id);
            if (slider != null)
            {
                if (!string.IsNullOrEmpty(updateSliderDto.AzTitle))
                {
                    slider.AzTitle = updateSliderDto.AzTitle;
                }
                if (!string.IsNullOrEmpty(updateSliderDto.EnTitle))
                {
                    slider.EnTitle = updateSliderDto.EnTitle;
                }
                if (!string.IsNullOrEmpty(updateSliderDto.RuTitle))
                {
                    slider.RuTitle = updateSliderDto.RuTitle;
                }
                if (!string.IsNullOrEmpty(updateSliderDto.AzDescription))
                {
                    slider.AzDescription = updateSliderDto.AzDescription;
                }
                if (!string.IsNullOrEmpty(updateSliderDto.EnDescription))
                {
                    slider.EnDescription = updateSliderDto.EnDescription;
                }
                if (!string.IsNullOrEmpty(updateSliderDto.RuDescription))
                {
                    slider.RuDescription = updateSliderDto.RuDescription;
                }

                _unitOfWork.Repository<Slider>().Update(slider);
                _unitOfWork.Commit();
                var response = _mapper.Map<GetSliderDto>(slider);
                return new ServiceResult(true, response.Id);
            }
            return new ServiceResult(false);
        }

        #endregion
    }
}
