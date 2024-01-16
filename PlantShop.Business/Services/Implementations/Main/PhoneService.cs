using AutoMapper;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using PlantShop.DataAccess.UnitOfWorks;
using PlantShop.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class PhoneService : IPhoneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhoneRepository _phoneRepository;

        public PhoneService(IPhoneRepository phoneRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _phoneRepository = phoneRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> AddPhone(string phoneDto)
        {
            var phone = await _phoneRepository.GetPhoneByNumber(phoneDto);
            if (phone == null)
            {
                string random = Helper.GetRandomString();
                var message = $"Sizin birdefelik tesdiq shifreniz " + random;
                var phones = new PhoneNumber { Code = random, Phone = phoneDto, IsActive = true };
                await _unitOfWork.Repository<PhoneNumber>().AddAsync(phones);
                _unitOfWork.Commit();
                await Helper.SendSmsAsync(phoneDto, message);
                return new ServiceResult(true, "send");

            }
            else
            {
              
                var time = DateTime.Now - phone.ActiveDate;
                var day = time.Days;
                if (day <10 && phone.IsConfirmed==true)
                {
                    return new ServiceResult(true, "confirmed");
                }
               
                    string random = Helper.GetRandomString();
                    var message = $"Sizin birdefelik tesdiq shifreniz " + random;
                      phone.Code = random;
                    _unitOfWork.Commit();
                    await Helper.SendSmsAsync(phone.Phone, message);
                    return new ServiceResult(true, "send");
              
               
            }
            throw new NotImplementedException();
        }
    }
}
