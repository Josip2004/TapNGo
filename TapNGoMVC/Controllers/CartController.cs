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
        public IActionResult Add(int itemId, string name, decimal price, int? categoryId)
        {
            _cartService.AddItem(itemId, name, price);

            if (categoryId.HasValue)
                TempData["LastCategoryId"] = categoryId;

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
    }
}
