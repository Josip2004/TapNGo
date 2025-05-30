using TapNGo.DAL.Models;

namespace TapNGo.DAL.Repositories.Orders
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Add(Order item);
        void Update(Order item);
        void Delete(int id);
    }
}
