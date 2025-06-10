using TapNGo.DAL.Models;
using TapNGo.DAL.SessionModels;

namespace TapNGo.DAL.Services.OrderService
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order? GetOrder(int id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        public int CreateOrderWithItems(List<CartItem> items, int tableNum, int? userId = null, string? note = null);
    }
}
