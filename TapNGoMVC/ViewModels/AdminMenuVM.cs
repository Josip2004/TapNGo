using TapNGo.DAL.Models;

namespace TapNGoMVC.ViewModels
{
    public class AdminMenuVM
    {
        public List<MenuCategory> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
        public List<MenuVM> MenuItems { get; set; }
    }
}
