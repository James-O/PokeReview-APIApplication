using PokeReview.Data;
using PokeReview.Interfaces;
using PokeReview.Models;

namespace PokeReview.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReviewRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool CreateReview(Review review)
        {
            _appDbContext.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _appDbContext.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _appDbContext.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _appDbContext.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _appDbContext.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _appDbContext.Reviews.Where(r=>r.Pokemon.Id==pokeId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _appDbContext.Reviews.Any(r=> r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _appDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _appDbContext.Update(review);
            return Save();
        }
    }
}
