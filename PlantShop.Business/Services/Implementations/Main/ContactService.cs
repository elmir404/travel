using AutoMapper;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using PlantShop.DataAccess.UnitOfWorks;
using System.Reflection;
using System;
using Microsoft.AspNetCore.Mvc;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;
        private readonly IPhoneRepository _phoneRepository;
        public ContactService(IContactRepository contactRepository, IMapper mapper, IUnitOfWork unitOfWork, IPhoneRepository phoneRepository)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _phoneRepository = phoneRepository;
        }
        public async Task<ServiceResult> AddContact(AddContactDto contact)
        {
           var phone=await _phoneRepository.GetPhoneByNumber(contact.Number);
            if (phone != null)
            {
                var time = DateTime.Now - phone.ActiveDate;
                var day = time.Days;
                if (phone.IsConfirmed==true && day<10)
                {
                    
                    var request = new Contact {Message = contact.Message, Number = phone, Subject = contact.Subject, Name = contact.Name};
                    await _unitOfWork.Repository<Contact>().AddAsync(request);
                    _unitOfWork.Commit();
                    return new ServiceResult(true, request);
                }
                if(phone.Code==contact.Code)
                {
                    var request = new Contact { Message = contact.Message, Number = phone, Subject = contact.Subject, Name = contact.Name };
                    phone.IsConfirmed = true;
                    phone.ActiveDate = DateTime.Now;
                    await _unitOfWork.Repository<Contact>().AddAsync(request);
                    _unitOfWork.Commit();
                    return new ServiceResult(true, request);
                }
                else
                {
                    return new ServiceResult(false, "code");
                }
               
            }
            return new ServiceResult(false, "error");
        }
       

        public async Task<ServiceResult> DeleteContactById(int contactId)
        {
            var request = _unitOfWork.Repository<Contact>().CheckExist(x => x.Id == contactId);

            if (request == false)
                return new ServiceResult(false, "Contact not found");

            _unitOfWork.Repository<Contact>().Delete(x => x.Id == contactId);
            _unitOfWork.Commit();
            return new ServiceResult(true);
        }

        public async Task<ServiceResult> GetContact(int contactId)
        {
            try
            {
                var result = await _contactRepository.GetContact(contactId);
                if (result == null)
                    return new ServiceResult(false, "data not found");

                var response = _mapper.Map<GetContactDto>(result);
                return new ServiceResult(true, response);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetContacts()
        {
            var request = await _unitOfWork.Repository<Contact>().GetAllAsync();
            var response = _mapper.Map<List<GetContactDto>>(request);
            if (request == null)
                return new ServiceResult(false);

            return new ServiceResult(true, response);
        }
    }
}
