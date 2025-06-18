using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TapNGo.DAL.Services.OrderService;
using TapNGo.DAL.Services.UserService;
using TapNGo.DAL.SessionModels;
using TapNGo.DAL.SessionServices;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly IUserService _userService;
        private readonly ICartItemService _cartService;
        private readonly IMapper _mapper;
        private HubConnection _hubConnection;

        public OrderController(IOrderService service,
            ICartItemService cartService,
            IUserService userService,
            IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
            _userService = userService;
            _cartService = cartService;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            int? tableNum = HttpContext.Session.GetInt32("TableNumber");

            if (!tableNum.HasValue)
            {
                TempData["Error"] = "Oprostite, ovu funkciju možete koristiti samo dok ste u kafiću.";
                return RedirectToAction("Index", "Menu");
            }

            var items = _cartService.GetItems();
            var total = items.Sum(i => i.Quantity * i.Price);
            ViewBag.Total = total;

            return View(items);
        }

        [HttpPost]
        public IActionResult Clear()
        {
            _cartService.SaveCart(new List<CartItem>());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(string? note)
        {
            int? tableNum = HttpContext.Session.GetInt32("TableNumber");

            if (!tableNum.HasValue)
            {
                TempData["Error"] = "Oprostite, ovu funkciju možete koristiti samo dok ste u kafiću.";
                return RedirectToAction("Index", "Menu");
            }
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5235/orderHub")
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.StartAsync().GetAwaiter().GetResult();



            var items = _cartService.GetItems();
            if (!items.Any())
                return RedirectToAction("Index", "Menu");

            int? userId = null;

            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = _userService.GetByUsername(username);
                if (user != null)
                    userId = user.Id;
            }

            int orderId = _service.CreateOrderWithItems(items, tableNum.Value, userId, note);
            _cartService.SaveCart(new List<CartItem>());


            _hubConnection.InvokeAsync("NotifyNewOrder", orderId)
                .GetAwaiter()
                .GetResult();

            TempData["Message"] = "Vaša narudžba je zaprimljena!";
            TempData["ShowReviewModal"] = true;
            TempData["NewOrderId"] = orderId;

            int? categoryId = null;
            if (int.TryParse(Request.Form["categoryId"], out int parsedCategoryId))
            {
                categoryId = parsedCategoryId;
            }

            return RedirectToAction("Index", "Menu", new { categoryId = categoryId });
        }
    }
}
