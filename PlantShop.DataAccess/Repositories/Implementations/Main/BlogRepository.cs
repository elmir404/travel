
using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.DataAccess.Repositories.Implementations
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(PlantShopDbContext educationDbContext) : base(educationDbContext)
        {

        }

        public async Task<ICollection<Blog>> GetBlogs()
        {
            return await GetAsQueryable().Include(x => x.BlogFiles).ToListAsync();
        }

        public async Task<Blog> GetBlog(int blogId)
        {
            return await GetAsQueryable().Include(x => x.BlogFiles).FirstOrDefaultAsync(x => x.Id == blogId);
        }

        public async Task<ICollection<Blog>> GetActiveBlogs()
        {
            return await GetAsQueryable()
                .Where(x => x.IsActive == true)
                .Include(x => x.BlogFiles).ToListAsync();
        }

        public async Task<ICollection<Blog>> GetActiveBlogsWithoutFile()
        {
            return await GetAsQueryable()
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }

        public async Task<Blog> GetBlogBySlug(string slug)
        {
            return await GetAsQueryable().Include(x => x.BlogFiles).FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public IQueryable<Blog> SearchBlogs()
        {
            var query = GetAsQueryable()
               .Include(x => x.BlogFiles)
               .Where(x => x.IsActive);
            return query.OrderBy(x => x.Id);
        }
    }
}
