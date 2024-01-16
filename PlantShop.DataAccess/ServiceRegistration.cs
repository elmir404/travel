using Microsoft.Extensions.DependencyInjection;
using PlantShop.DataAccess.Repositories;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using PlantShop.DataAccess.Repositories.Implementations.Main;
using PlantShop.DataAccess.UnitOfWorks;

namespace PlantShop.DataAccess
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IAboutCompanyRepository, AboutCompanyRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
