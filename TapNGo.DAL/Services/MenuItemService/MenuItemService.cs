using TapNGo.DAL.Models;
using TapNGo.DAL.Repositories.MenuItems;

namespace TapNGo.DAL.Services.MenuItemService
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _repository;

        public MenuItemService(IMenuItemRepository repository)
        {
            _repository = repository;
        }
        public void CreateMenuItem(MenuItem item) => _repository.Add(item);

        public void DeleteMenuItem(int id) => _repository.Delete(id);

        public IEnumerable<MenuItem> GetAllMenuItems() => _repository.GetAll(); 

        public MenuItem? GetMenuItem(int id) => _repository.GetById(id);

        public void UpdateMenuItem(MenuItem item) => _repository.Update(item);

        public IEnumerable<MenuItem> GetWithCategory() => _repository.GetWithCategory();
        public IEnumerable<MenuItem> GetItemsByCategoryId(int categoryId) => _repository.GetItemsByCategoryId(categoryId);
    }
}
