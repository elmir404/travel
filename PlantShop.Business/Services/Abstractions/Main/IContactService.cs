using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;

namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IContactService
    {
        Task<ServiceResult> AddContact(AddContactDto contact);
        Task<ServiceResult> DeleteContactById(int contactId);
        Task<ServiceResult> GetContact(int contactId);
        Task<ServiceResult> GetContacts();
    }
}
