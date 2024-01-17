using AutoMapper;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Helpers;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions;
using PlantShop.DataAccess.UnitOfWorks;


namespace Portfolio.Business.Services.Implementations.Main
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper, IBlogRepository blogRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blogRepository = blogRepository;
        }

        public async Task<ServiceResult> AddBlog(AddBlogDto blog)
        {
            var request = _mapper.Map<Blog>(blog);
            request.IsActive = true;
            await _unitOfWork.Repository<Blog>().AddAsync(request);
            _unitOfWork.Commit();
            var response = _mapper.Map<GetBlogDto>(request);
            var productSluq = blog.AzTitle.Slugify();
            Random rand = new Random();
            var random = rand.Next(100);
            request.Slug = productSluq + "_" +random;
            _unitOfWork.Repository<Blog>().Update(request);
            _unitOfWork.Commit();
            return new ServiceResult(true, response.Id);
        }


        public async Task<ServiceResult> UpdateBlog(AddBlogDto blogDto, int id)
        {
            var blog = await _blogRepository.GetBlog(id);
            //if (blog != null)
            //{
            //    if (!blogDto.AzTitle.IsNullOrEmpty())
            //    {
            //        blog.AzTitle = blogDto.AzTitle;
            //        var productSluq = blogDto.AzTitle.Slugify();
            //        Random rand = new Random();
            //        var random = rand.Next(100);
            //        blog.Slug = productSluq + "_" + random;
            //    }

            //    if (!blogDto.EnName.IsNullOrEmpty())
            //    {
            //        blog.EnName = blogDto.EnName;
            //    }

            //    if (!blogDto.RuName.IsNullOrEmpty())
            //    {
            //        blog.RuName = blogDto.RuName;
            //    }
            //    if (!blogDto.AzTitle.IsNullOrEmpty())
            //    {
            //        blog.AzTitle = blogDto.AzTitle;
            //    }

            //    if (!blogDto.EnTitle.IsNullOrEmpty())
            //    {
            //        blog.EnTitle = blogDto.EnTitle;
            //    }

            //    if (!blogDto.RuTitle.IsNullOrEmpty())
            //    {
            //        blog.RuTitle = blogDto.RuTitle;
            //    }  
            //    if (!blogDto.AzDate.IsNullOrEmpty())
            //    {
            //        blog.AzDate = blogDto.AzDate;
            //    }

            //    if (!blogDto.EnDate.IsNullOrEmpty())
            //    {
            //        blog.EnDate = blogDto.EnDate;
            //    }

            //    if (!blogDto.RuDate.IsNullOrEmpty())
            //    {
            //        blog.RuDate = blogDto.RuDate;
            //    }

            //    if (!blogDto.AzDescription.IsNullOrEmpty())
            //    {
            //        blog.AzDescription = blogDto.AzDescription;
            //    }

            //    if (!blogDto.EnDescription.IsNullOrEmpty())
            //    {
            //        blog.EnDescription = blogDto.EnDescription;
            //    }

            //    if (!blogDto.RuDescription.IsNullOrEmpty())
            //    {
            //        blog.RuDescription = blogDto.RuDescription;
            //    }
            //    _unitOfWork.Repository<Blog>().Update(blog);
            //    _unitOfWork.Commit();
            //    var response = _mapper.Map<GetBlogDto>(blog);
            //    return new ServiceResult(true, response.Id);
            //}
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> DeleteBlogById(int blogId)
        {
            var blog = await _blogRepository.GetBlog(blogId);
            if (blog != null)
            {
                blog.IsActive = false;
                _unitOfWork.Repository<Blog>().Update(blog);
                _unitOfWork.Commit();
                return new ServiceResult(true);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> GetBlog(int blogId)
        {
            try
            {
                var result = await _blogRepository.GetBlog(blogId);
                if (result == null)
                    return new ServiceResult(false, "data not found");

                var response = _mapper.Map<GetBlogDto>(result);
                return new ServiceResult(true, response);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetBlogs()
        {
            var blogs = await _blogRepository.GetBlogs();
            if (blogs != null)
            {
                var response = _mapper.Map<ICollection<GetBlogDto>>(blogs);
                return new ServiceResult(true, response);
            }

            return new ServiceResult(false);
        }

        public async Task<ServiceResult> GetActiveBlogs()
        {
            var blogs = await _blogRepository.GetActiveBlogs();
            if (blogs != null)
            {
                var response = _mapper.Map<ICollection<GetBlogDto>>(blogs);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> GetActiveBlogsWithouttFile()
        {
            
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> GetBlogBySlug(string slug)
        {
            try
            {
                var result = await _blogRepository.GetBlogBySlug(slug);
                if (result == null)
                    return new ServiceResult(false, "data not found");

                var response = _mapper.Map<GetBlogDto>(result);
                return new ServiceResult(true, response);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> SearchBlogs(int currentPage, int pageSize)
        {
            if (pageSize > 100)
                pageSize = 100;

          
            var query = _blogRepository.SearchBlogs();

            var page = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            SearchResult<IEnumerable<GetBlogDto>> response = new SearchResult<IEnumerable<GetBlogDto>>()
            {
                Value = _mapper.Map<IEnumerable<GetBlogDto>>(page.Select(p => p)),
                DataCount = query.Count(),
                CurrentPage = currentPage,
                PageSize = pageSize
            };

            return new ServiceResult(true, response);
        }
    }
}
