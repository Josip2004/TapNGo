using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using TapNGo.DAL.Models;
using TapNGo.DAL.Services.CategoryService;
using TapNGo.DAL.Services.MenuItemService;
using TapNGo.DAL.Services.UserService;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.Controllers
{
    public class AdminMenuController : Controller
    {
        private readonly IMenuItemService _service;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public AdminMenuController(IMenuItemService service,
            ICategoryService categoryService,
            IUserService userService,
            IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _userService = userService;
            _mapper = mapper;
        }

        // GET: AdminMenuController
        public ActionResult Index(int? categoryId)
        {
            var categories = _categoryService.GetAllCategories().ToList();

            int selectedCategoryId;

            if (categoryId.HasValue)
                selectedCategoryId = categoryId.Value;
            else if (categories.Any())
                selectedCategoryId = categories.First().Id;
            else 
                selectedCategoryId = 0;

            var menuItems = _service.GetItemsByCategoryId(selectedCategoryId);
            var menuItemVM = _mapper.Map<List<MenuVM>>(menuItems);

            var viewModel = new AdminMenuVM
            {
                Categories = categories,
                SelectedCategoryId = selectedCategoryId,
                MenuItems = menuItemVM
            };


            return View(viewModel);
        }

        // GET: AdminMenuController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: AdminMenuController/Create
        public ActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();
            var users = _userService.GetAllUsers();

            var model = new MenuEditVM
            {
                Categories = categories
                 .Select(c => new SelectListItem
                 {
                     Text = c.Name,
                     Value = c.Id.ToString()
                 }).ToList(),
                Users = users
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.FirstName + " " + u.LastName
                    }).ToList()
            };

            return View(model);
        }

        // POST: AdminMenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuEditVM menuVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    PopulateItemTypes(menuVm);
                    PopulateUsers(menuVm);

                    return View(menuVm);    
                }

                if (menuVm.ImageFile != null && menuVm.ImageFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    menuVm.ImageFile.CopyTo(ms);
                    var imageBytes = ms.ToArray();
                    menuVm.ImageUrl = $"data:{menuVm.ImageFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}";
                }

                var newMenu = _mapper.Map<MenuItem>(menuVm);

                _service.CreateMenuItem(newMenu);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                PopulateItemTypes(menuVm);

                return View(menuVm);
            }
        }

        // GET: AdminMenuController/Edit/5
        public ActionResult Edit(int id)
        {
            var menuItem = _service.GetAllMenuItems()
          .FirstOrDefault(mi => mi.Id == id);

            if (menuItem == null)
                return NotFound();

            var menuVm = _mapper.Map<MenuEditVM>(menuItem);
            PopulateItemTypes(menuVm);
            PopulateUsers(menuVm);

            return View(menuVm);
        }

        // POST: AdminMenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MenuEditVM menuVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    PopulateItemTypes(menuVm);
                    PopulateUsers(menuVm);

                    return View(menuVm);
                }

                if (menuVm.ImageFile != null && menuVm.ImageFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    menuVm.ImageFile.CopyTo(ms);
                    var imageBytes = ms.ToArray();
                    menuVm.ImageUrl = $"data:{menuVm.ImageFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}";
                }

                var menuDb = _service.GetMenuItem(menuVm.Id);   

                if (menuDb == null)
                    return NotFound();

                _mapper.Map(menuVm, menuDb);

                _service.UpdateMenuItem(menuDb);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(menuVm);
            }
        }
        private void PopulateItemTypes(MenuEditVM menu)
        {
            menu.Categories = _categoryService.GetAllCategories()
                   .Select(c => new SelectListItem
                   {
                       Value = c.Id.ToString(),
                       Text = c.Name
                   }).ToList();
        }

        private void PopulateUsers(MenuEditVM menu)
        {
            menu.Users = _userService.GetAllUsers()
                   .Select(c => new SelectListItem
                   {
                       Value = c.Id.ToString(),
                       Text = c.FirstName + " " + c.LastName
                   }).ToList();
        }

        // POST: AdminMenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var menu = _service.GetMenuItem(id);

                if (menu == null)
                    return NotFound();

                _service.DeleteMenuItem(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
