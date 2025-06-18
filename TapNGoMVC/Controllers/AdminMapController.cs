using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapNGo.DAL.Models;
using TapNGo.DAL.Services.OrderService;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.Controllers
{
    public class AdminMapController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        public AdminMapController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetOrder(int id)
        {
            var userDb = _orderService.GetOrder(id);
            return Json(
            new
            {
                userDb?.Id,
                userDb?.TableNumber,
                userDb?.UserId,
                userDb?.Note,
                userDb?.Status,
                userDb?.TotalPrice
            });
        }

        public OrderVM GetOrderByTable(int id)
        {
            var orders = _orderService.GetAllOrders()
                .Where(o => o.TableNumber == id)
                .OrderByDescending(o => o.Id)
                .ToList();
            if (orders.Count > 0)
            {
                var order = orders.First();
                return _mapper.Map<OrderVM>(order);
            }
            else
            {
                return new OrderVM
                {
                    Id = 0,
                    TableNumber = id,
                    UserId = null,
                    Note = null,
                    Status = 0,
                    TotalPrice = 0
                };
            }
        }
    }
}
