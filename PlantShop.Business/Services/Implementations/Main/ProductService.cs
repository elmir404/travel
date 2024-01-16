using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Helpers;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Models;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using PlantShop.DataAccess.UnitOfWorks;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        #region Get
        public async Task<ServiceResult> GetActiveProducts()
        {
            try
            {
                var products = await _productRepository.GetActiveProducts();
                if (products != null)
                {
                    var response = _mapper.Map<ICollection<GetProductDto>>(products);
                    return new ServiceResult(true, response);
                }
                return new ServiceResult(false, "data not found");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }
        public async Task<ServiceResult> GetDeactiveProducts()
        {
            try
            {
                var products = await _productRepository.GetDeactiveProducts();

                if (products != null)
                {
                    var response = _mapper.Map<ICollection<GetProductDto>>(products);
                    return new ServiceResult(true, response);
                }
                return new ServiceResult(false, "data not found");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }
        public async Task<ServiceResult> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product != null)
                {
                    var response = _mapper.Map<GetProductDto>(product);
                    return new ServiceResult(true, response);
                }
                return new ServiceResult(false, "data not found");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }
        public async Task<ServiceResult> GetProductForUpdate(int id)
        {
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product != null)
                {
                    var response = _mapper.Map<UpdateProductDto>(product);
                    return new ServiceResult(true, response);
                }
                return new ServiceResult(false, "data not found");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }
        public async Task<ServiceResult> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProducts();
                if (products != null)
                {
                    var response = _mapper.Map<ICollection<GetProductDto>>(products);
                    return new ServiceResult(true, response);
                }
                return new ServiceResult(false);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }
        #endregion

        #region Post
        public async Task<ServiceResult> ActiveProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product != null)
            {
                product.IsActive = true;
                _unitOfWork.Repository<Product>().Update(product);
                _unitOfWork.Commit();
                return new ServiceResult(true);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> AddProduct(AddProductDto product)
        {
            var request = _mapper.Map<Product>(product);
            request.IsActive = true;
            var name = await _productRepository.GetProductNameCondition(product.AzName);
            if (name == true)
            {
                await _unitOfWork.Repository<Product>().AddAsync(request);
                var productSluq = product.AzName.Slugify();
                request.Slug = productSluq;
                var str = (int)((product.UpPrice - product.Price) / product.UpPrice * 100);
                request.Inrst = str;
                _unitOfWork.Commit();
                var response = _mapper.Map<GetProductDto>(request);
                return new ServiceResult(true, response.Id);
            }
            return new ServiceResult(false, "Bu adda product var");
        }
        public async Task<ServiceResult> DeactiveProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product != null)
            {
                product.IsActive = false;
                _unitOfWork.Repository<Product>().Update(product);
                _unitOfWork.Commit();
                return new ServiceResult(true);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product != null)
            {
                _unitOfWork.Repository<Product>().Delete(product);
                _unitOfWork.Commit();
                return new ServiceResult(true);
            }
            return new ServiceResult(false);
        }
        public async Task<ServiceResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetProduct(updateProductDto.Id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(updateProductDto.AzName))
                {
                    product.AzName = updateProductDto.AzName;
                    var productSluq = product.AzName.Slugify();
                    product.Slug = productSluq;
                }
                if (!string.IsNullOrEmpty(updateProductDto.EnName))
                {
                    product.EnName = updateProductDto.EnName;
                }
                if (!string.IsNullOrEmpty(updateProductDto.RuName))
                {
                    product.RuName = updateProductDto.RuName;
                }

                if (!string.IsNullOrEmpty(updateProductDto.AzDescription))
                {
                    product.AzDescription = updateProductDto.AzDescription;
                }
                if (!string.IsNullOrEmpty(updateProductDto.EnDescription))
                {
                    product.EnDescription = updateProductDto.EnDescription;
                }
                if (!string.IsNullOrEmpty(updateProductDto.RuDescription))
                {
                    product.RuDescription = updateProductDto.RuDescription;
                }

                if (updateProductDto.Price != null)
                {
                    product.Price = updateProductDto.Price;
                } if (updateProductDto.UpPrice != null)
                {
                    product.UpPrice = updateProductDto.UpPrice;
                }
                if (updateProductDto.CategoryId != null)
                {
                    product.CategoryId = updateProductDto.CategoryId;
                }
                var str = (int)((updateProductDto.UpPrice - updateProductDto.Price) / updateProductDto.UpPrice * 100);
                product.Inrst = str;
                _unitOfWork.Repository<Product>().Update(product);
                _unitOfWork.Commit();
                var response = _mapper.Map<GetProductDto>(product);
                return new ServiceResult(true, response.Id);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> SearchProduct(int currentPage, int pageSize,SearchModelDto searchModelDto)
        {
            if (pageSize > 100)
                pageSize = 100;

            var search = _mapper.Map<HomeSearchModel>(searchModelDto);
            var query = _productRepository.SearchProduct(search);

            var page = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            SearchResult<IEnumerable<GetProductDto>> response = new SearchResult<IEnumerable<GetProductDto>>()
            {
                Value = _mapper.Map<IEnumerable<GetProductDto>>(page.Select(p => p)),
                DataCount = query.Count(),
                CurrentPage = currentPage,
                PageSize = pageSize
            };

            return new ServiceResult(true, response);
        }

        public async Task<ServiceResult> GetProductBySlug(string slug)
        {
            try
            {
                var product = await _productRepository.GetProductBySlug(slug);
                if (product != null)
                {
                    var response = _mapper.Map<GetProductDto>(product);
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
    }
}
