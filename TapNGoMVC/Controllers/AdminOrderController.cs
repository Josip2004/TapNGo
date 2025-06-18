using AutoMapper;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.Services.OrderService;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public AdminOrderController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: AdminOrderController
        public ActionResult Index()
        {
            var orders = _service.GetAllOrders().OrderByDescending(t => t.Id);

            var model = new AdminOrderVM
            {
                Orders = _mapper.Map<List<OrderVM>>(orders)
            };

            return View(model);
        }

        // GET: AdminOrderController/Details/5
        public ActionResult Details(int id)
        {
            var order = _service.GetOrder(id);

            if (order == null)
                NotFound();

            var orderVm = _mapper.Map<OrderVM>(order);

            return View(orderVm);
        }



        // GET: AdminOrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: AdminOrderController/Create
        [HttpPost]
        public ActionResult Confirm(int orderId)
        {
            _service.DeleteOrder(orderId);
            return RedirectToAction(nameof(Index));

        }

        // POST: AdminOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminOrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminOrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
