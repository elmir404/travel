using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlantShop.Business.Dtos.Get.Main;
using PlantShop.Business.Dtos.Post.Main;
using PlantShop.Business.Results;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Migrations;
using PlantShop.DataAccess.Repositories.Abstractions.Main;
using PlantShop.DataAccess.Repositories.Implementations.Main;
using PlantShop.DataAccess.UnitOfWorks;
using PlantShop.Infrastructure.Extensions;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMailService _mailService;
        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IProductRepository productRepository, IOrderRepository orderRepository, IMailService mailService, IPhoneRepository phoneRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _session = httpContextAccessor.HttpContext.Session;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mailService = mailService;
            _phoneRepository = phoneRepository;
        }
        #region Get
        public async Task<ServiceResult> GetOder(int id)
        {
            try
            {
                var result = await _orderRepository.GetOrder(id);
                if (result == null)
                    return new ServiceResult(false, "data not found");

                var response = _mapper.Map<GetOrderDto>(result);
                return new ServiceResult(true, response);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetOrders()
        {
            var orders = await _orderRepository.GetOrders();
            if (orders != null)
            {
                var response = _mapper.Map<ICollection<GetOrderDto>>(orders);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> GetActiveOrders()
        {
            var orders = await _orderRepository.GetActiveOrders();
            if (orders != null)
            {
                var response = _mapper.Map<ICollection<GetOrderDto>>(orders);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }
        #endregion

        #region Post
        public async Task<ServiceResult> AddOrder(AddOrderDto addOrderDto)
        {
            var request = new Order {Email=addOrderDto.Email,Message=addOrderDto.Message,Name=addOrderDto.Name};

            request.ProductOrders = new List<ProductOrder>();
            var phone = addOrderDto.Number.Replace(" ", "");
           
            var ids = addOrderDto.OrderProducts.Select(x => x.Id).ToList();

            var products = _productRepository.GetAll(x => ids.Any(c => c == x.Id)).ToList();
            double totalPrice = 0;
            double totalUpPrice = 0;
            foreach (var product in addOrderDto.OrderProducts)
            {
                var prod = products.FirstOrDefault(x => x.Id == product.Id);
                totalPrice = totalPrice + prod.Price*product.Count;
                totalUpPrice = totalUpPrice + (double)prod.UpPrice * product.Count;
                if (prod != null)
                {
                    var productlist = new ProductOrder { ProductId = product.Id,ProductName=prod.AzName, Count = product.Count, Price = prod.Price,UpPrice=(double)prod.UpPrice,Inrst =(int)prod.Inrst};
                    request.ProductOrders.Add(productlist);
                }
            }
            request.TotalPrice=totalPrice;
            request.TotalUpPrice=totalUpPrice;
            var str = (int)((totalUpPrice - totalPrice) / totalUpPrice * 100);
            request.Inrst=str;
            var mobile = await _phoneRepository.GetPhoneByNumber(addOrderDto.Number);
           
            request.Status = 1;
            request.isActive = true;
            if (mobile == null || mobile.IsConfirmed==false ) {
                string random = Helper.GetRandomString();
                var message = $"Sizin birdefelik tesdiq shifreniz " + random;
                var phones = new PhoneNumber { Code = random, Phone = addOrderDto.Number,IsActive=true,ActiveDate = DateTime.Now };
                request.Number= phones;
                mobile.Code = random;
                await Helper.SendSmsAsync(addOrderDto.Number, message);
                await _unitOfWork.Repository<Order>().AddAsync(request);
                _unitOfWork.Commit();
                var response = _mapper.Map<GetOrderDto>(request);

                return new ServiceResult(true,response.Id);

            }
            if(mobile!=null &&mobile.IsConfirmed==true  ) {
                var time = DateTime.Now - mobile.ActiveDate;
                var day = time.Days;
                if (day > 10 )
                {
                    string random1 = Helper.GetRandomString();
                    var message1 = $"Sizin birdefelik tesdiq shifreniz " + random1;
                    var phones = new PhoneNumber {Id=mobile.Id,Code = random1,IsActive = true,Phone = addOrderDto.Number,IsConfirmed=false, };
                    request.Number = phones;
                    await Helper.SendSmsAsync(addOrderDto.Number, message1);
                     _unitOfWork.Repository<Order>().Update(request);
                    _unitOfWork.Commit();
                    var response1 = _mapper.Map<GetOrderDto>(request);
                    return new ServiceResult(true, response1.Id);
                }
               
                request.numberId = mobile.Id;
                string random = Helper.GetRandomString();
                var message = $"Sifarisiniz qeyd olundu,endirim ucun promo kodunuz:" + random;
                request.PromoCode = random;
                await Helper.SendSmsAsync(addOrderDto.Number, message);
                 await _unitOfWork.Repository<Order>().AddAsync(request);
                _unitOfWork.Commit();
               
                var response = _mapper.Map<GetOrderDto>(request);
                var adminMessage = $"Adı: {request.Name} Telefon: {addOrderDto.Number} ID:{response.Id} Promo: {random}, Endirimisiz Mebleg:{request.TotalUpPrice} Endirimli {request.TotalPrice} Faiz:{request.Inrst}";
                await Helper.SendSmsAsync("0515262222", adminMessage);
                return new ServiceResult(true,"confirm");


            }
            return new ServiceResult(false, "phone_false");




        }

        public async Task<ServiceResult> OrderConfirmation(int id,string code)
        {
            var order = await _orderRepository.GetOrder(id);
            var phone = await _phoneRepository.GetPhoneByNumber(order.Number.Phone);
            if(phone != null && phone.IsConfirmed == false)
            {
                if (phone.Code.ToLower() == code.ToLower())
                {
                    string random = Helper.GetRandomString();
                    var message = $"Sifarisiniz qeyd olundu,endirim ucun promo kodunuz:" + random;
                    var adminMessage = $"Adı: {order.Name} Telefon: {order.Number} Id: {id} Promo: {random}, Endirimisiz Mebleg:{order.TotalUpPrice} Endirimli {order.TotalPrice} Faiz:{order.Inrst}";
                    await Helper.SendSmsAsync("0515262222",adminMessage);
                    order.PromoCode = random;
                    phone.ActiveDate = DateTime.Now;
                    phone.IsConfirmed = true;
                    await Helper.SendSmsAsync(phone.Phone, message);
                    order.isConfirmed = true;
                    _unitOfWork.Commit();

                    return new ServiceResult(true, "sifariş tamamlandı");
                }
                return new ServiceResult(false, "kod error");
            }

            return new ServiceResult(false, "error");
        }

        public async Task<ServiceResult> GetActiveOrdersByStatus(int status)
        {
            var orders = await _orderRepository.GetActiveOrdersByStatus(status);
            if (orders != null)
            {
                var response = _mapper.Map<ICollection<GetOrderDto>>(orders);
                return new ServiceResult(true, response);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> DeactiveOrder(int id)
        {
            var order = await _orderRepository.GetOrder(id);
            if (order != null)
            {
               

                order.isActive = false;
                _unitOfWork.Repository<Order>().Update(order);
                _unitOfWork.Commit();
                return new ServiceResult(true, order);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> changeStatus(int id, int status)
        {
            var order = await _orderRepository.GetOrder(id);
            if (order != null)
            {


                order.Status = status;
                _unitOfWork.Repository<Order>().Update(order);
                _unitOfWork.Commit();
                return new ServiceResult(true, order);
            }
            return new ServiceResult(false);
        }

        public async Task<ServiceResult> GetOderByPhone(string phone)
        {
            try
            {
                var result = await _orderRepository.GetOrderByPhone(phone);
                if (result == null)
                    return new ServiceResult(false, "data not found");

                var response = _mapper.Map<GetOrderDto>(result);
                return new ServiceResult(true, response);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }



        #endregion
    }
}
