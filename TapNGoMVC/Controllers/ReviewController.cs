using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.Models;
using TapNGo.DAL.Services.ReviewService;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;
        public ReviewController(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult SubmitReview([FromBody] ReviewVM reviewVm)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            var review = _mapper.Map<Review>(reviewVm);
            review.UserId = null;

            _service.CreateReview(review);

            return Ok();
        }
    }
}
