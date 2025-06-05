using TapNGo.DAL.Models;
using TapNGo.DAL.SessionModels;

namespace TapNGo.DAL.Repositories.Orders
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Add(Order item);
        void Update(Order item);
        void Delete(int id);
        public void CreateOrderWithItems(List<CartItem> items, int? userId = null, string? note = null);
    }
}
