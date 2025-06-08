using TapNGo.DAL.Models;

namespace TapNGo.DAL.Repositories.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TapNgoV1Context _context;

        public ReviewRepository(TapNgoV1Context context)
        {
            _context = context;
        }

        public void Add(Review item)
        {
            _context.Reviews.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Reviews
                .FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                _context.Reviews.Remove(item);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        public Review? GetById(int id)
        {
            return _context.Reviews.Find(id);
        }

        public void Update(Review item)
        {
            _context.Reviews.Update(item);
            _context.SaveChanges();
        }
    }
}
