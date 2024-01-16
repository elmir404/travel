using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Business.Services.Abstractions.Main;
using PlantShop.Business.Services.Implementations.Main;
using System.Data;

namespace PlantShop.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPut]
        public async Task<IActionResult> Deactive(int id)
        {
            var result = await _orderService.DeactiveOrder(id);
            if (result.Success)
                return Ok(result);

            return BadRequest("Error occured");
        } 
        [HttpPost]
        public async Task<IActionResult> ChageStatus(int id,int status)
        {
            var result = await _orderService.changeStatus(id,status);
            if (result.Success)
                return Ok(result);
            return BadRequest("Error occured");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.GetOder(id);
            return View(order.Data);
        }


        public async Task<IActionResult> List(int status)
        {
            var orders = await _orderService.GetActiveOrdersByStatus(status);
            return View(orders.Data);
        }
    }
}
