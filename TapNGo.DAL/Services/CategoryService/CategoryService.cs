using TapNGo.DAL.Models;
using TapNGo.DAL.Repositories.Categories;

namespace TapNGo.DAL.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public void CreateCategory(MenuCategory category) => _repository.Add(category);

        public void DeleteCategory(int id) => _repository.Delete(id);   

        public IEnumerable<MenuCategory> GetAllCategories() => _repository.GetAll();

        public MenuCategory? GetCategory(int id) => _repository.GetById(id);

        public void UpdateCategory(MenuCategory category) => _repository.Update(category);  
    }
}
