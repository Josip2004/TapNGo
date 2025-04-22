using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapNGo.DTOs;
using TapNGo.Models;

namespace TapNGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly TapNgoV1Context _context;

        public ReviewController(TapNgoV1Context context)
        {
            _context = context; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReviewResponseDTO>> GetAllReviews([FromQuery] int? userId)
        {
            try
            {

                var reviews = _context.Reviews
                .Select(r => new ReviewResponseDTO
                {
                    Rating = r.Rating,
                    Comment = r.Comment
                })
                .ToList();

                return Ok(reviews);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]

        public ActionResult<ReviewResponseDTO> GetReviewById(int idReview)
        {
            try
            {
                var review = _context.Reviews
                       .FirstOrDefault(r => r.Id == idReview);

                if (review == null)
                {

                    return NotFound();
                }
                return Ok(review);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<ReviewCreateDTO> CreateReview([FromBody] ReviewCreateDTO dto)
        {
            try
            {

                var user = _context.Users.Find(dto.UserId);
                if (user == null) return BadRequest("User not exists");

                var order = _context.Orders.Find(dto.OrderId);
                if (order == null) return BadRequest("Order not exists");

                if (order.UserId != dto.UserId) return BadRequest("User can´t review someone else´s order");

                Review review = new();
                review.Order = order;
                review.UserId = dto.UserId;
                review.Rating = dto.Rating;
                review.Comment = dto.Comment;

                _context.Reviews.Add(review);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetReviewById), new { idReview = review.Id }, null);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public ActionResult<ReviewResponseDTO> DeleteReview(int id)
        {
            try
            {

                var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

                if (review == null)
                    return NotFound();

                _context.Reviews.Remove(review);
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
