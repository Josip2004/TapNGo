using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.DAL.Repositories.MenuItems;
using TapNGo.DAL.Repositories.Orders;
using TapNGo.Models;

namespace TapNGo.DAL.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void CreateOrder(Order order) => _repository.Add(order);

        public void DeleteOrder(int id) => _repository.Delete(id);  

        public IEnumerable<Order> GetAllOrders() => _repository.GetAll();   

        public Order? GetOrder(int id) => _repository.GetById(id);

        public void UpdateOrder(Order order) => _repository.Update(order);
    }
}
