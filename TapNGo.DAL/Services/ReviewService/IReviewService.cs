using TapNGo.DAL.Models;

namespace TapNGo.DAL.Services.ReviewService
{
    public interface IReviewService
    {
        IEnumerable<Review> GetAllReviews();
        Review? GetReview(int id);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int id);
    }
}
