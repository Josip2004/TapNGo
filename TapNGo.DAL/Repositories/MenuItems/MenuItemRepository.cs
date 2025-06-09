using Microsoft.EntityFrameworkCore;
using TapNGo.DAL.Models;

namespace TapNGo.DAL.Repositories.MenuItems
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly TapNgoV1Context _context;

        public MenuItemRepository(TapNgoV1Context context)
        {
            _context = context;
        }

        public void Add(MenuItem item)
        {
            _context.MenuItems.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.MenuItems
                .Include(h => h.OrderItems)
                .FirstOrDefault(h => h.Id == id);

            if (item != null)
            {
                _context.OrderItems.RemoveRange(item.OrderItems);
                _context.MenuItems.Remove(item);

                _context.SaveChanges();
            }
        }

        public IEnumerable<MenuItem> GetAll()
        {
            return _context.MenuItems.ToList();
        }

        public MenuItem? GetById(int id)
        {
            return _context.MenuItems.Find(id);
        }

        public void Update(MenuItem item)
        {
            _context.MenuItems.Update(item);
            _context.SaveChanges();
        }

        public IEnumerable<MenuItem> GetWithCategory()
        {
            return _context.MenuItems
             .Include(m => m.MenuCategory);
        }

        public IEnumerable<MenuItem> GetItemsByCategoryId(int categoryId)
        {
            return _context.MenuItems
               .Where(item => item.MenuCategoryId == categoryId)
               .ToList();
        }
    }
}
