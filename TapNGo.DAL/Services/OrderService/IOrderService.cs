using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.Models;

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
