using PlantShop.DataAccess.Entities.Base;
using PlantShop.DataAccess.Repositories;

namespace PlantShop.DataAccess.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;

        void Commit();
    }
}
