using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.Services.CategoryService;
using TapNGo.DAL.Services.MenuItemService;
using TapNGo.DAL.SessionServices;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuItemService _service;
        private readonly ICartItemService _cartService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public MenuController(IMenuItemService service,
            ICartItemService cartService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
            _cartService = cartService;
            _categoryService = categoryService;
        }
        // GET: MenuController
        public ActionResult Index(int categoryId)
        {
            var categories = _categoryService.GetAllCategories()
                    .FirstOrDefault(c => c.Id == categoryId);

            var items = _service.GetAllMenuItems()
                    .Where(i => i.MenuCategoryId == categoryId)
                    .ToList();

            var itemVM = _mapper.Map<List<MenuVM>>(items);

            var cartItems = _cartService.GetItems();

            foreach (var ci in cartItems)
            {
                var match = cartItems.FirstOrDefault(ci => ci.MenuItemId == ci.MenuItemId);
                if (match != null)
                {
                    ci.Quantity = match.Quantity;
                }
            }

            return View(itemVM);
        }

        // GET: MenuController/Details/5
        public ActionResult Details(int id)
        {
            var item = _service.GetWithCategory()
                    .FirstOrDefault(i => i.Id == id);

            if (item == null)
                return NotFound();

            var itemVM = _mapper.Map<MenuVM>(item); 

            return View(itemVM);
        }

        // GET: MenuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuController/Create
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

        // GET: MenuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuController/Edit/5
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

        // GET: MenuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuController/Delete/5
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
