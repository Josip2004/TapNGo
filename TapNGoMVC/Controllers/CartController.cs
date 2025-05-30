using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.SessionModels;
using TapNGo.DAL.SessionServices;

namespace TapNGoMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartItemService _cartService;

        public CartController(ICartItemService cartService)
        {
            _cartService = cartService;
        }

        // GET: CartController
        public ActionResult Index()
        {
            var items = _cartService.GetItems();
            return View(items);
        }

        [HttpPost]
        public IActionResult Add(int itemId, string name, decimal price)
        {
            _cartService.AddItem(itemId, name, price);
            return Ok(); 
        }

        [HttpPost]
        public IActionResult Remove(int itemId)
        {
            _cartService.RemoveItem(itemId);
            return Ok(); 
        }

        [HttpPost]
        public IActionResult Clear()
        {
            _cartService.SaveCart(new List<CartItem>());
            return RedirectToAction("Index");
        }

        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
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

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
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

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
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
