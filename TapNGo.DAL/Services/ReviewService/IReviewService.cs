using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.Models;

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
