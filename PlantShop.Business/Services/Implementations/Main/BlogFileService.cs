using AutoMapper;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Abstractions.Storage;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class BlogFileService : IBlogFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorage _storage;
        private readonly IMapper _mapper;

        public BlogFileService(IUnitOfWork unitOfWork, IStorage storage, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storage = storage;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddRangeAsync(AddBlogDto blogDto, int blogId)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storage.UploadAsync("photo-blog", blogDto.Files);
            var blog = _unitOfWork.Repository<Blog>().Get(x => x.Id == blogId);
            _unitOfWork.Repository<BlogFile>().AddRange(result.Select(x => new BlogFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Blog = blog
            }));
            _unitOfWork.Commit();
            return new ServiceResult(true);
        }

      

        public async Task<ServiceResult> UpdateRangeAsync(AddBlogDto blogDto, int blogId)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storage.UploadAsync("photo-blog", blogDto.Files);
            var blog = _unitOfWork.Repository<Blog>().Get(x => x.Id == blogId);
            var entity = await _unitOfWork.Repository<BlogFile>().GetAllAsync(x => x.Blog.Id == blogId);
            _unitOfWork.Repository<BlogFile>().DeleteRange(entity);
            _unitOfWork.Repository<BlogFile>().UpdateRange(result.Select(x => new BlogFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Blog = blog
            }));
            _unitOfWork.Commit();
            return new ServiceResult(true);
        }

       
    }
}
