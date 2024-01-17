using Microsoft.Extensions.DependencyInjection;
using PlantShop.Business.Mappings;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Abstractions.Storage;
using PlantShop.Business.Services.Abstractions.Users;
using PlantShop.Business.Services.Implementations.Main;
using PlantShop.Business.Services.Implementations.Storage;
using PlantShop.Business.Services.Implementations.Users;
using Portfolio.Business.Services.Implementations.Main;

namespace PlantShop.Business
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IStorage, FileStorage>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IAboutCompanyService, AboutCompanyService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IBlogFileService, BlogFileService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddAutoMapper(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MapProfile>();
            });
            //services.AddAutoMapper(typeof(MapProfile).GetType().Assembly);
        }
    }
}
