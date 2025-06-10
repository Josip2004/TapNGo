using Microsoft.EntityFrameworkCore;
using TapNGo.DAL.Models;
using TapNGo.DAL.SessionModels;

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
            return _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .FirstOrDefault(o => o.Id == id);
        }

        public void Update(Order item)
        {
            _context.Orders.Update(item);   
            _context.SaveChanges();
        }

        public int CreateOrderWithItems(List<CartItem> items, int tableNum, int? userId, string? note = null)
        {
            var order = new Order
            {
                UserId = userId,
                Status = 1,
                Note = note,
                TotalPrice = items.Sum(i => i.Price * i.Quantity),
                TableNumber = tableNum,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                });
            }


            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

    }
}
