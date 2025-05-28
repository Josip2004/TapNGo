using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.Models;

namespace TapNGo.DAL.Repositories.Category
{
    public interface ICategoryRepository
    {
        IEnumerable<MenuCategory> GetAll();
        MenuCategory? GetById(int id);
        void Add(MenuCategory category);
        void Update(MenuCategory category);
        void Delete(int id);
    }
}
