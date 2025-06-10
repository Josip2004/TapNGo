using AutoMapper;
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
        public ActionResult Index(int? categoryId, string? searchTerm)
        {

            var items = _service.GetAllMenuItems()
                   .AsQueryable();

            if (categoryId.HasValue && categoryId.Value != 0)
            {
                items = items.Where(i => i.MenuCategoryId == categoryId.Value);
            }


            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerTerm = searchTerm.ToLower();   
                items = items.Where(i => i.Name.ToLower().Contains(lowerTerm));
            }

            var itemVM = _mapper.Map<List<MenuVM>>(items.ToList());

            var cartItems = _cartService.GetItems();

            foreach (var item in itemVM)
            {
                var match = cartItems.FirstOrDefault(ci => ci.MenuItemId == item.Id);
                if (match != null)
                {
                    item.Quantity = match.Quantity;
                }
            }
            ViewBag.Total = cartItems.Sum(i => i.Price * i.Quantity);


            return View(itemVM);
        }
        //ZA PONUDU DANA:
        public IActionResult Specials()
        {
            return View();
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
    }
}
