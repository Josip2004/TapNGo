using TapNGo.DAL.Models;

namespace TapNGo.DAL.Services.MenuItemService
{
    public interface IMenuItemService
    {
        IEnumerable<MenuItem> GetAllMenuItems();
        MenuItem? GetMenuItem(int id);
        void CreateMenuItem(MenuItem item);
        void UpdateMenuItem(MenuItem item);
        void DeleteMenuItem(int id);
        IEnumerable<MenuItem> GetWithCategory();
        IEnumerable<MenuItem> GetItemsByCategoryId(int categoryId);
    }
}
