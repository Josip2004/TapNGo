using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.Models;
using TapNGo.DAL.Services.MenuItemService;
using TapNGo.DAL.Services.OrderService;
using TapNGo.DTOs;

namespace TapNGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IMenuItemService _menuItemService;

        public OrderController(IMapper mapper, IOrderService orderService, IMenuItemService menuItemService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _menuItemService = menuItemService;

        }

        // GET: api/Order
        [HttpGet]
        public ActionResult<IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            try
            {
                var orders = _orderService.GetAllOrders()
                    .Select(o => _mapper.Map<OrderResponseDTO>(o))
                    .ToList();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving orders: {ex.Message}");
            }
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public ActionResult<OrderResponseDTO> GetOrderById(int id)
        {
            try
            {
                var order = _orderService.GetOrder(id);

                if (order == null)
                    return NotFound();

                var response = _mapper.Map<OrderResponseDTO>(order);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the order: {ex.Message}");
            }
        }

        // POST: api/Order
        [HttpPost]
        public ActionResult<OrderResponseDTO> CreateOrder([FromBody] OrderCreateDTO dto)
        {
            try
            {
                foreach (var item in dto.OrderItems)
                {
                    if (item.Quantity < 1)
                        return BadRequest($"Quantity must be at least 1 for MenuItemId: {item.MenuItemId}.");
                }

                var order = _mapper.Map<Order>(dto);

                decimal totalPrice = 0;
                foreach (var item in order.OrderItems)
                {
                    var menuItem = _menuItemService.GetMenuItem(item.MenuItemId);
                    if (menuItem == null)
                        return BadRequest($"MenuItem with ID: {item.MenuItemId} not found.");

                    totalPrice += menuItem.Price * (item.Quantity ?? 1);
                }

                order.TotalPrice = totalPrice;

                _orderService.CreateOrder(order);

                var response = _mapper.Map<OrderResponseDTO>(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the order: {ex.Message}");
            }
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderUpdateDTO dto)
        {
            try
            {
                foreach (var item in dto.OrderItems)
                {
                    if (item.Quantity < 1)
                        return BadRequest($"Quantity must be at least 1 for MenuItemId: {item.MenuItemId}.");
                }

                var existingOrder = _orderService.GetOrder(id);

                if (existingOrder == null)
                    return NotFound();

                existingOrder.OrderItems.Clear();
                _mapper.Map(dto, existingOrder);

                decimal newTotal = 0;
                foreach (var item in existingOrder.OrderItems)
                {
                    var menuItem = item.MenuItem;
                    if (menuItem == null)
                        return BadRequest($"MenuItem with ID {item.MenuItemId} not found.");

                    newTotal += menuItem.Price * (item.Quantity ?? 1);
                }

                existingOrder.TotalPrice = newTotal;

                _orderService.UpdateOrder(existingOrder);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the order: {ex.Message}");
            }
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var order = _orderService.GetOrder(id);
                if (order == null)
                    return NotFound();

                _orderService.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the order: {ex.Message}");
            }
        }
    }
}
