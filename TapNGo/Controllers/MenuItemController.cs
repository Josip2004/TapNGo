using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.Models;
using TapNGo.DAL.Services.MenuItemService;
using TapNGo.DTOs;

namespace TapNGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _service;
        private readonly IMapper _mapper;

        public MenuItemController(IMenuItemService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MenuItemResponseDTO>> GetAllMenus([FromQuery] int? id)
        {
            try
            {
                var menu = _service.GetAllMenuItems();
                if (id != null)
                    menu = menu.Where(m => m.Id == id.Value);

               var dtos = menu.Select(m => _mapper.Map<MenuItemResponseDTO>(m)).ToList();

                if (!dtos.Any())
                    return NotFound();

                return Ok(dtos);
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
                var menuitem = _service.GetMenuItem(id);

                if (menuitem == null)
                {
                    return NotFound();
                }


                var dto = _mapper.Map<MenuItemCreateDTO>(menuitem);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<MenuItemCreateDTO> CreateMenu([FromBody] MenuItemCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            try
            {
                var menuItem = _mapper.Map<MenuItem>(dto);

                _service.CreateMenuItem(menuItem);

                var createdMenuItem = _service.GetMenuItem(menuItem.Id);

                var createdDTO = _mapper.Map<MenuItemCreateDTO>(createdMenuItem);

                return CreatedAtAction(nameof(GetMenuById), new { id = menuItem.Id }, createdDTO);
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
                var existingMenu = _service.GetMenuItem(id);
                if (existingMenu == null)
                    return NotFound();

                _mapper.Map(dto, existingMenu);

                _service.UpdateMenuItem(existingMenu);

                var updatedDTO = _mapper.Map<MenuItemUpdateDTO>(existingMenu);

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
                var menu = _service.GetMenuItem(id);
                if (menu == null)
                    return NotFound();

                _service.DeleteMenuItem(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
