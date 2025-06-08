using TapNGo.DAL.Models;
using TapNGo.DAL.Repositories.Reviews;

namespace TapNGo.DAL.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }

        public void CreateReview(Review review) => _repository.Add(review);

        public void DeleteReview(int id) => _repository.Delete(id);

        public IEnumerable<Review> GetAllReviews() => _repository.GetAll();

        public Review? GetReview(int id) => _repository.GetById(id);

        public void UpdateReview(Review review) => _repository.Update(review);
    }
}
