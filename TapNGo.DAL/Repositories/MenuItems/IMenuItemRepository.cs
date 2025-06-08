using TapNGo.DAL.Models;

namespace TapNGo.DAL.Repositories.MenuItems
{
    public interface IMenuItemRepository
    {
        IEnumerable<MenuItem> GetAll();
        MenuItem? GetById(int id);
        void Add(MenuItem item);
        void Update(MenuItem item);
        void Delete(int id);
        IEnumerable<MenuItem> GetWithCategory();
        IEnumerable<MenuItem> GetItemsByCategoryId(int categoryId);
    }
}
