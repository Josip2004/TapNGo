using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TapNGo.DAL.Services.CategoryService;
using TapNGo.DAL.Models;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();

            var model = new CategoryIndexVM
            {
                Categories = _mapper.Map<List<MenuCategoryVM>>(categories)
            };

            return View(model);
        }

        public IActionResult HomeQrCode(int table)
        {
            HttpContext.Session.SetInt32("TableNumber", table);

            return RedirectToAction("Index", "Category");
        }
    }
}
