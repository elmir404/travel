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
    public class AboutCompanyService : IAboutCompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAboutCompanyRepository _aboutCompanyRepository;
        public AboutCompanyService(IMapper mapper, IUnitOfWork unitOfWork, IAboutCompanyRepository aboutCompanyRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _aboutCompanyRepository = aboutCompanyRepository;
        }
        #region Get
        public async Task<ServiceResult> GetAboutCompany()
        {
            var aboutCompanies = await _aboutCompanyRepository.GetAboutCompany();
            if (aboutCompanies != null)
            {
                var response = _mapper.Map<GetAboutCompany>(aboutCompanies);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> GetAboutCompany(int id)
        {
            var aboutCompany = await _aboutCompanyRepository.GetAboutCompany(id);
            if (aboutCompany != null)
            {
                var response = _mapper.Map<GetAboutCompany>(aboutCompany);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> GetAboutCompanyForUpdate(int id)
        {
            try
            {
                var aboutCompany = await _aboutCompanyRepository.GetAboutCompany(id);
                if (aboutCompany != null)
                {
                    var response = _mapper.Map<UpdateAboutCompany>(aboutCompany);
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
        public async Task<ServiceResult> AddAboutCompany(AddAboutCompany addAboutCompany)
        {
            var request = _mapper.Map<AboutCompany>(addAboutCompany);
            await _unitOfWork.Repository<AboutCompany>().AddAsync(request);
            _unitOfWork.Commit();
            var response = _mapper.Map<GetAboutCompany>(request);
            return new ServiceResult(true, response.Id);
        }
        public async Task<ServiceResult> UpdateAboutCompany(UpdateAboutCompany updateAboutCompany)
        {
            var aboutCompany = await _aboutCompanyRepository.GetAboutCompany(updateAboutCompany.Id);
            if (aboutCompany != null)
            {
                if (updateAboutCompany.Address != null)
                {
                    aboutCompany.Address = updateAboutCompany.Address;
                }
                
                if (updateAboutCompany.MapAddress != null)
                {
                    aboutCompany.MapAddress = updateAboutCompany.MapAddress;
                }

                if (updateAboutCompany.Number != null)
                {
                    aboutCompany.Number = updateAboutCompany.Number;
                }

                if (updateAboutCompany.Email != null)
                {
                    aboutCompany.Email = updateAboutCompany.Email;
                }

                aboutCompany.TwitterLink = updateAboutCompany.TwitterLink;
                aboutCompany.FacebookLink = updateAboutCompany.FacebookLink;
                aboutCompany.InstagramLink = updateAboutCompany.InstagramLink;

                _unitOfWork.Repository<AboutCompany>().Update(aboutCompany);
                _unitOfWork.Commit();
                var response = _mapper.Map<GetAboutCompany>(aboutCompany);
                return new ServiceResult(true, response.Id);
            }
            return new ServiceResult(false);
        }

        #endregion
    }
}
