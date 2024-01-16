using AutoMapper;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using PlantShop.DataAccess.UnitOfWorks;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        #region Get
        public async Task<ServiceResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            if (categories != null)
            {
                var response = _mapper.Map<ICollection<GetCategoryDto>>(categories);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> GetActiveCategories()
        {
            var categories = await _categoryRepository.GetActiveCategories();
            if (categories != null)
            {
                var response = _mapper.Map<ICollection<GetCategoryDto>>(categories);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> GetCategory(int id)
        {
            try
            {
                var result = await _categoryRepository.GetCategory(id);
                if (result == null)
                    return new ServiceResult(false, "data not found");

                var response = _mapper.Map<GetCategoryDto>(result);
                return new ServiceResult(true, response);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }
        public async Task<ServiceResult> GetDeactiveCategories()
        {
            var categories = await _categoryRepository.GetDeactiveCategories();
            if (categories != null)
            {
                var response = _mapper.Map<ICollection<GetCategoryDto>>(categories);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false); ;
        }
        #endregion

        #region Post
        public async Task<ServiceResult> ActiveCategory(int id)
        {
            var category = await _categoryRepository.ActiveCategory(id);
            if (category != null)
            {
                foreach (var product in category.Products)
                {
                    product.IsActive = true;
                }

                category.IsActive = true;
                _unitOfWork.Repository<Category>().Update(category);
                _unitOfWork.Commit();
                return new ServiceResult(true, category);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> AddCategory(AddCategoryDto category)
        {
            var request = _mapper.Map<Category>(category);
            request.IsActive = true;
            await _unitOfWork.Repository<Category>().AddAsync(request);
            _unitOfWork.Commit();
            var response = _mapper.Map<GetCategoryDto>(request);
            return new ServiceResult(true, response.Id);
        }
        public async Task<ServiceResult> DeactiveCategory(int id)
        {
            var category = await _categoryRepository.DeactiveCategory(id);
            if (category != null)
            {
                foreach (var product in category.Products)
                {
                    product.IsActive = false;
                }
                
                category.IsActive = false;
                _unitOfWork.Repository<Category>().Update(category);
                _unitOfWork.Commit();
                return new ServiceResult(true, category);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> UpdateCategory(AddCategoryDto categoryDto, int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            if (category != null)
            {
                if (categoryDto.AzName != null)
                {
                    category.AzName = categoryDto.AzName;
                }
                if (categoryDto.EnName != null)
                {
                    category.EnName = categoryDto.EnName;
                }
                if (categoryDto.RuName != null)
                {
                   category.RuName = categoryDto.RuName;
                }

                _unitOfWork.Repository<Category>().Update(category);
                _unitOfWork.Commit();
                var response = _mapper.Map<GetCategoryDto>(category);
                return new ServiceResult(true, response.Id);
            }
            return new ServiceResult(false);
        }
        #endregion
    }
}
