

using PlantShop.DataAccess.Context;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;

namespace PlantShop.DataAccess.Repositories.Implementations.Main;
public class BlogFileRepository : GenericRepository<BlogFile>, IBlogFileRepository
{
    public BlogFileRepository(PlantShopDbContext educationDbContext) : base(educationDbContext)
    {

    }
}
