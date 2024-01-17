
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;


namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IBlogService
    {
        #region Post
        Task<ServiceResult> AddBlog(AddBlogDto blog);
        Task<ServiceResult> DeleteBlogById(int blogId);
        Task<ServiceResult> UpdateBlog(AddBlogDto blogDto, int id);
        #endregion

        #region Get
        Task<ServiceResult> GetBlog(int blogId);
        Task<ServiceResult> GetBlogBySlug(string slug);
        Task<ServiceResult> GetBlogs();
        Task<ServiceResult> GetActiveBlogs();
        Task<ServiceResult> SearchBlogs(int currentPage, int pageSize);
        Task<ServiceResult> GetActiveBlogsWithouttFile();
        #endregion
    }
}
