using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.DAL.Models;
using TapNGo.Models;

namespace TapNGo.DAL.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly TapNgoV1Context _context;

        public UserRepository(TapNgoV1Context context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Users
                    .Include(i => i.Reviews)
                    .Include(i => i.Orders)
                    .Include(i => i.MenuItems)
                    .FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                _context.Orders.RemoveRange(item.Orders);
                _context.MenuItems.RemoveRange(item.MenuItems);
                _context.Reviews.RemoveRange(item.Reviews);
                _context.Users.Remove(item);

                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
