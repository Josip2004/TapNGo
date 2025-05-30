using TapNGo.DAL.Models;

namespace TapNGo.DAL.Repositories.Categories
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
