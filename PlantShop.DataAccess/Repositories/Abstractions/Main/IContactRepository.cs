using PlantShop.DataAccess.Entities.Main;

namespace PlantShop.DataAccess.Repositories.Abstractions.Main
{
    public interface IContactRepository 
    {
        Task<Contact> GetContact(int contactId);
    }
}
