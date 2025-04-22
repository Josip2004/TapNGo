using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var menu = _context.MenuItems
                    .Include(m => m.MenuCategory)
                    .Where(m => !categoryID.HasValue || m.MenuCategoryId == categoryID.Value)
                    .Select(m => new MenuItemResponseDTO
                    {
                        Name = m.Name,
                        Description = m.Description,
                        Price = m.Price
                    }).ToList();
            

            return Ok(menu);
        }

        [HttpGet("{id}")]
        public ActionResult<MenuItemResponseDTO> GetMenuById(int idMenuItem)
        {
            var menuItem = _context.MenuItems
                .Include(m => m.MenuCategory)
                .FirstOrDefault(m => m.Id == idMenuItem);

            if (menuItem == null)
            {
                return NotFound();
            }

            return Ok(menuItem);
        }

        [HttpPost]
        public ActionResult<MenuItemCreateDTO> CreateMenu([FromBody] MenuItemCreateDTO dto)
        {
            var user = _context.MenuItems.Find(dto.UserId);
            var category = _context.MenuItems.Find(dto.MenuCategoryId);

            if (user != null) return BadRequest("User not found");
            if (category != null) return BadRequest("Category not found");

            MenuItem menuItem = new();

            menuItem.Name = dto.Name;
            menuItem.Description = dto.Description;
            menuItem.Price = dto.Price;

            _context.MenuItems.Add(menuItem);   
            _context.SaveChanges();

            User userMenu = new();

            userMenu.Id = menuItem.UserId;

            MenuCategory categoryMenu = new MenuCategory();

            categoryMenu.Id = dto.MenuCategoryId;

            _context.Users.Add(userMenu);
            _context.MenuCategories.Add(categoryMenu);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMenuById), new { id = menuItem.Id }, menuItem);
        }

        [HttpPut]
        public ActionResult<MenuItemUpdateDTO> UpdateMenu(int id,
            [FromBody] MenuItemUpdateDTO dto)
        {
            var existingMenu =  _context.MenuItems.Find(id);
            if (existingMenu == null)
                return NotFound();


            existingMenu.Name = dto.Name;
            existingMenu.Description = dto.Description;
            existingMenu.ImageUrl = dto.ImageUrl;
            existingMenu.Price = dto.Price;
            existingMenu.MenuCategoryId = dto.MenuCategoryId;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public ActionResult<MenuItemResponseDTO> DeleteMenuItem(int id)
        {
            var menu = _context.MenuItems.Find(id);
            if (menu == null)
                return NotFound();


            _context.Users.RemoveRange(menu.User);
            _context.MenuCategories.RemoveRange(menu.MenuCategory);
            _context.MenuItems.Remove(menu);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
