using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.DAL.Models;
using TapNGo.Models;

namespace TapNGo.DAL.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TapNgoV1Context _context;

        public OrderRepository(TapNgoV1Context context)
        {
            _context = context;
        }

        public void Add(Order item)
        {
            _context.Orders.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Orders
                .Include(i => i.Reviews)
                .Include(i => i.OrderItems)
                .FirstOrDefault(h => h.Id == id);

            if (item != null)
            {
                _context.OrderItems.RemoveRange(item.OrderItems);
                _context.Reviews.RemoveRange(item.Reviews);
                _context.Orders.Remove(item);

                _context.SaveChanges();
            }
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order? GetById(int id)
        {
            return _context.Orders.Find(id);
        }

        public void Update(Order item)
        {
            _context.Orders.Update(item);   
            _context.SaveChanges();
        }
    }
}
