using PokeReview.Data;
using PokeReview.Interfaces;
using PokeReview.Models;

namespace PokeReview.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _appDbContext;

        public OwnerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool CreateOwner(Owner owner)
        {
            _appDbContext.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _appDbContext.Remove(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return _appDbContext.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _appDbContext.PokemonOwners.Where(p=>p.Pokemon.Id==pokeId).Select(o=>o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _appDbContext.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _appDbContext.PokemonOwners.Where(o => o.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _appDbContext.Owners.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _appDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _appDbContext.Update(owner);
            return Save();
        }
    }
}
