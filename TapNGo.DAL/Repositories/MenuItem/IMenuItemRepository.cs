using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.Models;

namespace TapNGo.DAL.Repositories.MenuItems
{
    public interface IMenuItemRepository
    {
        IEnumerable<MenuItem> GetAll();
        MenuItem? GetById(int id);
        void Add(MenuItem item);
        void Update(MenuItem item);
        void Delete(int id);
    }
}
