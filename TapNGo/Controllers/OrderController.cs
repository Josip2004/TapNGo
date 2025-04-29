using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapNGo.DTOs;
using TapNGo.Models;

namespace TapNGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly TapNgoV1Context _context;

        public OrderController(TapNgoV1Context context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public ActionResult<IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            try
            {
                var orders = _context.Orders
                    .Select(o => new OrderResponseDTO
                    {
                        Id = o.Id,
                        UserId = o.UserId,
                        Status = o.Status,
                        TotalPrice = o.TotalPrice,
                        Note = o.Note
                    }).ToList();

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
                var order = _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .FirstOrDefault(o => o.Id == id);

                if (order == null)
                    return NotFound();

                var response = new OrderResponseDTO
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    Note = order.Note,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDetailDTO
                    {
                        MenuItemId = oi.MenuItemId,
                        Quantity = oi.Quantity ?? 1,
                        MenuItemName = oi.MenuItem?.Name // Assuming this property exists
                    }).ToList()

                };

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
                var user = _context.Users.Find(dto.UserId);
                if (user == null)
                    return BadRequest("User not found");

                decimal totalPrice = 0;
                var orderItems = new List<OrderItem>();

                foreach (var item in dto.OrderItems)
                {
                    var menuItem = _context.MenuItems.Find(item.MenuItemId);
                    if (menuItem == null)
                        return BadRequest($"MenuItem with ID {item.MenuItemId} not found");

                    totalPrice += menuItem.Price * item.Quantity;

                    orderItems.Add(new OrderItem
                    {
                        MenuItemId = item.MenuItemId,
                        Quantity = item.Quantity
                    });
                }

                var order = new Order
                {
                    UserId = dto.UserId,
                    Note = dto.Note,
                    Status = 0, // default status
                    TotalPrice = totalPrice,
                    OrderItems = orderItems
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                var orderResponse = new OrderResponseDTO
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    Note = order.Note,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDetailDTO
                    {
                        MenuItemId = oi.MenuItemId,
                        Quantity = oi.Quantity ?? 1,
                        MenuItemName = oi.MenuItem?.Name // Assuming this property exists
                    }).ToList()

                };

                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderResponse);
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
                var order = _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefault(o => o.Id == id);

                if (order == null)
                    return NotFound();

                order.Status = dto.Status;
                order.Note = dto.Note;

                if (dto.OrderItems != null && dto.OrderItems.Any())
                {
                    _context.OrderItems.RemoveRange(order.OrderItems);
                    order.OrderItems.Clear();

                    decimal newTotal = 0;
                    foreach (var item in dto.OrderItems)
                    {
                        var menuItem = _context.MenuItems.Find(item.MenuItemId);
                        if (menuItem == null)
                            return BadRequest($"MenuItem with ID {item.MenuItemId} not found");

                        newTotal += menuItem.Price * item.Quantity;

                        order.OrderItems.Add(new OrderItem
                        {
                            MenuItemId = item.MenuItemId,
                            Quantity = item.Quantity
                        });
                    }

                    order.TotalPrice = newTotal;
                }

                _context.SaveChanges();
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
                var order = _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefault(o => o.Id == id);

                if (order == null)
                    return NotFound();

                _context.OrderItems.RemoveRange(order.OrderItems);
                _context.Orders.Remove(order);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the order: {ex.Message}");
            }
        }
    }
}
