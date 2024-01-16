using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IPhoneService
    {
        #region Post
        Task<ServiceResult> AddPhone(string addPhoneDto);
     

        #endregion
    }
}
