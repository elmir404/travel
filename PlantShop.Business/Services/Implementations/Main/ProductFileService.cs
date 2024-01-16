using AutoMapper;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Abstractions.Storage;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.UnitOfWorks;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class ProductFileService : IProductFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorage _storage;
        private readonly IMapper _mapper;

        public ProductFileService(IMapper mapper, IUnitOfWork unitOfWork, IStorage storage)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _storage = storage;
        }

        public async Task<ServiceResult> AddRangeAsync(AddProductDto product, int productId)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storage.UploadAsync("photo-product", product.Files);
            var products = _unitOfWork.Repository<Product>().Get(x => x.Id == productId);
            _unitOfWork.Repository<ProductFile>().AddRange(result.Select(x => new ProductFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Product = products
            }));
            _unitOfWork.Commit();
            return new ServiceResult(true);
        }

        public async Task<ServiceResult> UpdateRangeAsync(UpdateProductDto updateProductDto)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storage.UploadAsync("photo-product", updateProductDto.Files);
            var products = _unitOfWork.Repository<Product>().Get(x => x.Id == updateProductDto.Id);
            var entity = await _unitOfWork.Repository<ProductFile>().GetAllAsync(x => x.Product.Id == updateProductDto.Id);
            _unitOfWork.Repository<ProductFile>().DeleteRange(entity);
            _unitOfWork.Repository<ProductFile>().UpdateRange(result.Select(x => new ProductFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Product = products
            }));
            _unitOfWork.Commit();
            return new ServiceResult(true);
        }
    }
}
