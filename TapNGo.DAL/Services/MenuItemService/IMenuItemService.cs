using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.Models;

namespace TapNGo.DAL.Services.MenuItemService
{
    public interface IMenuItemService
    {
        IEnumerable<MenuItem> GetAllMenuItems();
        MenuItem? GetMenuItem(int id);
        void CreateMenuItem(MenuItem item);
        void UpdateMenuItem(MenuItem item);
        void DeleteMenuItem(int id);
    }
}
