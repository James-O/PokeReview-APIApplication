using PokeReview.Data;
using PokeReview.Interfaces;
using PokeReview.Models;

namespace PokeReview.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly AppDbContext _appDbContext;
        public PokemonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            //first fetch the relationships
            var owner = _appDbContext.Owners.Where(o=>o.Id==ownerId).FirstOrDefault();
            var category = _appDbContext.Categories.Where(c=>c.Id==categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = owner,
                Pokemon = pokemon
            };
             
            _appDbContext.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _appDbContext.Add(pokemonCategory);

            _appDbContext.Add(pokemon);

            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _appDbContext.Remove(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            //return _appDbContext.Pokemon.FirstOrDefault(p=> p.Id == id);
            return _appDbContext.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _appDbContext.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _appDbContext.Reviews.Where(p=>p.Pokemon.Id==pokeId);
            if(review.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r=>r.Rating)/review.Count());
           
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _appDbContext.Pokemon.OrderBy(p=>p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
           return _appDbContext.Pokemon.Any(p=>p.Id==pokeId); 
        }

        public bool Save()
        {
            var saved = _appDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _appDbContext.Update(pokemon);
            return Save();
        }
    }
}
