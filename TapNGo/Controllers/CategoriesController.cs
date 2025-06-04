using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.Models;
using TapNGo.DAL.Services.CategoryService;
using TapNGo.DAL.Services.MenuItemService;
using TapNGo.DTOs;

namespace TapNGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMenuItemService _mservice;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService service,IMenuItemService mservice ,IMapper mapper)
        {
            _service = service;
            _mservice = mservice;
            _mapper = mapper;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryResponseDto>> GetAll()
        {
            try
            {
                var categories = _service.GetAllCategories()
                    .Select(c =>
                    _mapper.Map<CategoryResponseDto>(c))
                    .ToList();

                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database.Please try again later.");
            }
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public ActionResult<CategoryResponseDto> Get(int id)
        {
            try
            {
                var category = _service.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }

                var mappedCategory = _mapper.Map<CategoryResponseDto>(category);
                return Ok(mappedCategory);

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database.Please try again later.");
            }
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public ActionResult Post([FromBody] CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<MenuCategory>(categoryDto);
            if (category == null)
            {
                return BadRequest("Invalid category data.");
            }

            try
            {
                _service.UpdateCategory(category);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database.Please try again later.");

            }

            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public ActionResult<CategoryUpdateDto> Put(int id, [FromBody] CategoryUpdateDto value)
        {
            try
            {
                var category = _service.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }
                category.Name = value.Name;
                _mapper.Map(value, category);
                
                _service.UpdateCategory(category);

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database.Please try again later.");
            }
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public ActionResult<CategoryUpdateDto> Delete(int id)
        {
            try
            {
                var category = _service.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }

                var hasItems = _mservice.GetAllMenuItems().Where(m => m.Id == id);
                if (hasItems.Any())
                {
                    return BadRequest("Cannot delete category with existing items.");
                }

                _service.DeleteCategory(id);

                var dto = _mapper.Map<CategoryUpdateDto>(category);

                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database.Please try again later.");
            }
        }
    }
}
