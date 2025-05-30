using TapNGo.DAL.Models;

namespace TapNGo.DAL.Services.OrderService
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order? GetOrder(int id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
    }
}
