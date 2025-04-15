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
            var reviews = _context.Reviews
            .Include (r => r.User)
            .Where (r => !userId.HasValue || r.UserId == userId.Value)
            .Select(r => new ReviewResponseDTO { 
                Rating = r.Rating,
                Comment = r.Comment
            })
            .ToList(); 

            return Ok(reviews);
        }

        [HttpGet("{id}")]

        public ActionResult<ReviewResponseDTO> GetReviewById(int idReview)
        {
            var review = _context.Reviews
                .FirstOrDefault(r => r.Id == idReview);

            if (review == null) { 
                
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public ActionResult<ReviewCreateDTO> CreateReview([FromBody] ReviewCreateDTO dto)
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
        [HttpDelete("{id}")]
        public ActionResult<ReviewResponseDTO> DeleteReview(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
                return NotFound();

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
