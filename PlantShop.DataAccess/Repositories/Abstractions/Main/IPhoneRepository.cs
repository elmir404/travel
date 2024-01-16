using PlantShop.DataAccess.Entities.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.DataAccess.Repositories.Abstractions.Main
{
    public interface IPhoneRepository : IGenericRepository<PhoneNumber>
    {
        Task<IEnumerable<PhoneNumber>> GetAll();
        Task<IEnumerable<PhoneNumber>> GetActivePhones();
        Task<PhoneNumber> GetPhone(int id);
        Task<PhoneNumber> GetPhoneByNumber(string phone);
    }
}
