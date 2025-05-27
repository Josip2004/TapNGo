using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapNGo.DAL.Models;
using TapNGo.DTOs;
using TapNGo.Models;

namespace TapNGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly TapNgoV1Context _context;

        public MenuItemController(TapNgoV1Context context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MenuItemResponseDTO>> GetAllMenus([FromQuery] int? categoryID)
        {
            try
            {
                var menu = _context.MenuItems
                        .Include(m => m.MenuCategory)
                        .Where(m => !categoryID.HasValue || m.MenuCategoryId == categoryID.Value)
                        .Select(m => new MenuItemResponseDTO
                        {
                            Name = m.Name,
                            Description = m.Description,
                            Price = m.Price,
                            ImageUrl = m.ImageUrl
                        }).ToList();

                if (!menu.Any())
                    return NotFound();

                return Ok(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MenuItemResponseDTO> GetMenuById(int id)
        {
            try
            {
                var menuItem = _context.MenuItems
                    .Include(m => m.MenuCategory)
                    .FirstOrDefault(m => m.Id == id);

                if (menuItem == null)
                {
                    return NotFound();
                }

                return Ok(menuItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<MenuItemCreateDTO> CreateMenu([FromBody] MenuItemCreateDTO dto)
        {
            try
            {
                MenuItem menuItem = new()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    UserId = dto.UserId,
                    MenuCategoryId = dto.MenuCategoryId,
                    ImageUrl = dto.ImageUrl
                };

                _context.MenuItems.Add(menuItem);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetMenuById), new { id = menuItem.Id }, menuItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<MenuItemUpdateDTO> UpdateMenu(int id, [FromBody] MenuItemUpdateDTO dto)
        {
            try
            {
                var existingMenu = _context.MenuItems.Find(id);
                if (existingMenu == null)
                    return NotFound();

                existingMenu.Name = dto.Name;
                existingMenu.Description = dto.Description;
                existingMenu.ImageUrl = dto.ImageUrl;
                existingMenu.Price = dto.Price;
                existingMenu.MenuCategoryId = dto.MenuCategoryId;

                _context.SaveChanges();

                return Ok(existingMenu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<MenuItemResponseDTO> DeleteMenuItem(int id)
        {
            try
            {
                var menu = _context.MenuItems.Find(id);
                if (menu == null)
                    return NotFound();

                _context.MenuItems.Remove(menu);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
