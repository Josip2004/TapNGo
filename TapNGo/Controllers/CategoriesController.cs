using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TapNGo.DTOs;
using TapNGo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TapNGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly TapNgoV1Context _context;
        private readonly IMapper _mapper;
        public CategoriesController(TapNgoV1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryResponseDto>> GetAll()
        {
            try
            {
                var categories = _context.MenuCategories
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
                var category = _context.MenuCategories.FirstOrDefault(c => c.Id == id);
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
                _context.MenuCategories.Add(category);
                _context.SaveChanges();
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
                var category = _context.MenuCategories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                category.Name = value.Name;
                _context.SaveChanges();
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
                var category = _context.MenuCategories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return NotFound();
                }

                var hasItems = _context.MenuItems.Any(m => m.Id == id);
                if (hasItems)
                {
                    return BadRequest("Cannot delete category with existing items.");
                }

                _context.MenuCategories.Remove(category);
                _context.SaveChanges();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database.Please try again later.");
            }
        }
    }
}
