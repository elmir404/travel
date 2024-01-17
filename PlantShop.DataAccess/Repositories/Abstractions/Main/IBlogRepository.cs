

using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.DataAccess.Repositories.Abstractions
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<ICollection<Blog>> GetBlogs();
        Task<ICollection<Blog>> GetActiveBlogs();
        Task<Blog> GetBlog(int blogId);
        Task<Blog> GetBlogBySlug(string slug);
        IQueryable<Blog> SearchBlogs();
        Task<ICollection<Blog>> GetActiveBlogsWithoutFile();
    }
}
