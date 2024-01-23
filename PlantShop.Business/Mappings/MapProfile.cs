using AutoMapper;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Get.Users;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Dtos.Post.Users;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Entities.Users;
using PlantShop.DataAccess.Models;

namespace PlantShop.Business.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        #region User
        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        CreateMap<RegisterUserDto, ApplicationUser>().ReverseMap();
        CreateMap<ApplicationUser, RegisterUserDto>().ReverseMap();
        CreateMap<ApplicationUser, LoginUserDto>().ReverseMap();
        CreateMap<ApplicationUser, UserSessionDto>().ReverseMap();
        CreateMap<LoginUserDto, ApplicationUser>().ReverseMap();
        CreateMap<ApplicationRole, ApplicationRoleDto>().ReverseMap();
        #endregion

        #region Main
        CreateMap<Category, AddCategoryDto>().ReverseMap();
        CreateMap<Category, GetCategoryDto>().ReverseMap();
        CreateMap<Blog, GetBlogDto>().ReverseMap();
        CreateMap<Blog, AddBlogDto>().ReverseMap();
        CreateMap<Order, AddOrderDto>()
            .ReverseMap();
        CreateMap<Product, GetProductDto>().ReverseMap();
        //CreateMap<AddOrderProductDto, ProductOrder>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<Product, AddProductDto>().ReverseMap();
        CreateMap<About, GetAboutDto>().ReverseMap();
        CreateMap<About, AddAboutDto>().ReverseMap();
        CreateMap<About, UpdateAboutDto>().ReverseMap();
        CreateMap<AboutCompany, GetAboutCompany>().ReverseMap();
        CreateMap<AboutCompany, AddAboutCompany>().ReverseMap();
        CreateMap<AboutCompany, UpdateAboutCompany>().ReverseMap();
        CreateMap<Contact, GetContactDto>().ReverseMap();
        CreateMap<Contact, AddContactDto>().ReverseMap();
        CreateMap<ProductFile, GetFileDto>().ReverseMap();
        CreateMap<SliderFile, GetFileDto>().ReverseMap();
        CreateMap<Slider, GetSliderDto>().ReverseMap();
        CreateMap<Slider, AddSliderDto>().ReverseMap();
        CreateMap<Slider, UpdateSliderDto>().ReverseMap();
        CreateMap<Order, GetOrderDto>().ReverseMap();
        CreateMap<Order, GetOrderDto>().ReverseMap();
        CreateMap<ProductOrder, GetProductOrder>().ReverseMap();
        CreateMap<PhoneNumber, GetPhoneDto>().ReverseMap();
        CreateMap<HomeSearchModel, SearchModelDto>().ReverseMap();
        #endregion
    }
}
