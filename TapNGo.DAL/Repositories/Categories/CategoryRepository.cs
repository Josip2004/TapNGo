using Microsoft.EntityFrameworkCore;
using TapNGo.DAL.Models;

namespace TapNGo.DAL.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TapNgoV1Context _context;

        public CategoryRepository(TapNgoV1Context context)
        {
            _context = context;
        }

        public void Add(MenuCategory category)
        {
            _context.MenuCategories.Add(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.MenuCategories
              .Include(h => h.MenuItems)
              .FirstOrDefault(h => h.Id == id);

            if (item != null)
            {
                _context.MenuItems.RemoveRange(item.MenuItems);
                _context.MenuCategories.Remove(item);

                _context.SaveChanges();
            }
        }

        public IEnumerable<MenuCategory> GetAll()
        {
            return _context.MenuCategories.ToList();
        }

        public MenuCategory? GetById(int id)
        {
            return _context.MenuCategories.Find(id);
        }

        public void Update(MenuCategory category)
        {
            _context.MenuCategories.Update(category);
            _context.SaveChanges();
        }
    }
}
