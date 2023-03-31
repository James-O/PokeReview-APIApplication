using Microsoft.EntityFrameworkCore;
using PokeReview.Data;
using PokeReview.Interfaces;
using PokeReview.Models;

namespace PokeReview.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReviewerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _appDbContext.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _appDbContext.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _appDbContext.Reviewers.Where(r => r.Id == reviewerId).Include(r=>r.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _appDbContext.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _appDbContext.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _appDbContext.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool Save()
        {
            var saved = _appDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _appDbContext.Update(reviewer);
            return Save();
        }
    }
}
