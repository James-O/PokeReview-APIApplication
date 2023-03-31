using PokeReview.Data;
using PokeReview.Interfaces;
using PokeReview.Models;
using System.Linq;

namespace PokeReview.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext=appDbContext;
        }
        public bool CategoryExists(int id)
        {
            return _appDbContext.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            //change tracker - adding, updateing, modifying
            //state could be connected 99% of times and disconnected 1% of times   *EntityState.Added-disconnected state
            //if you are not within a dbcontext that is when you are in disconnected state
           
            _appDbContext.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _appDbContext.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _appDbContext.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            //return _appDbContext.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _appDbContext.PokemonCategories.Where(p => p.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _appDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _appDbContext.Update(category);
            return Save();
        }
    }
}
