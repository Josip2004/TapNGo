using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.Models;

namespace TapNGo.DAL.Repositories.Reviews
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAll();
        Review? GetById(int id);
        void Add(Review item);
        void Update(Review item);
        void Delete(int id);
    }
}
