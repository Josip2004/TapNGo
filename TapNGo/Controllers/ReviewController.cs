using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.Models;
using TapNGo.DAL.Services.OrderService;
using TapNGo.DAL.Services.ReviewService;
using TapNGo.DAL.Services.UserService;
using TapNGo.DTOs;

namespace TapNGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;
        private readonly IUserService _uservice;
        private readonly IOrderService _oservice;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService service,IUserService userService,IOrderService orderService,IMapper mapper)
        {
            _service = service;
            _uservice = userService;
            _oservice = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReviewResponseDTO>> GetAllReviews()
        {
            try
            {

                var reviews = _service.GetAllReviews();
                var reviewDTO = reviews.Select(r => _mapper.Map<ReviewResponseDTO>(r)).ToList();    

                return Ok(reviewDTO);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error in fetching reviews" + ex.Message);
            }
        }

        [HttpGet("{id}")]

        public ActionResult<ReviewResponseDTO> GetReviewById(int id)
        {
            try
            {
                var review = _service.GetReview(id);

                if (review == null)
                {

                    return NotFound("Did not found any review by that id");
                }

                var dto = _mapper.Map<ReviewResponseDTO>(review);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error in fetching reviews by Id" + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<ReviewCreateDTO> CreateReview([FromBody] ReviewCreateDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data");
            }
            try
            {

               
                var user = _uservice.GetUser(dto.UserId);
                if (user == null)  return BadRequest("User not exists");

                var order = _oservice.GetOrder(dto.OrderId);
                if (order == null) return BadRequest("Order not exist");

                if (order.UserId != dto.UserId) return BadRequest("User can´t review someone else´s order");


                var review = _mapper.Map<Review>(dto);

                _service.CreateReview(review);

                var respone = _mapper.Map<ReviewCreateDTO>(review);

                return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, respone);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error in creating review" + ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public ActionResult<ReviewResponseDTO> DeleteReview(int id)
        {
            try
            {

                var review = _service.GetReview(id);

                if (review == null)
                    return NotFound();

                _service.DeleteReview(id);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error in deleting review" + ex.Message);
            }
        }
    }
}
