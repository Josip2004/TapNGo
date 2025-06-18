using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TapNGo.DAL.Services.ReviewService;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.Controllers
{
    public class AdminReviewController : Controller
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public AdminReviewController(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var reviews = _service.GetAllReviews().OrderByDescending(r => r.Id);

            var model = new AdminReviewVM
            {
                Reviews = _mapper.Map<List<ReviewVM>>(reviews)
            };

            return View(model);
        }
    }
}
