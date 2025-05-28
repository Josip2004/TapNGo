using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.Models;

namespace TapNGo.DAL.Services.CategoryService
{
    public interface ICategoryService
    {
        IEnumerable<MenuCategory> GetAllCategories();
        MenuCategory? GetCategory(int id);
        void CreateCategory(MenuCategory category);
        void UpdateCategory(MenuCategory category);
        void DeleteCategory(int id);
    }
}
