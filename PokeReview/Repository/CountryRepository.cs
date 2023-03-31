using AutoMapper;
using PokeReview.Data;
using PokeReview.Dto;
using PokeReview.Interfaces;
using PokeReview.Models;

namespace PokeReview.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CountryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public bool CountryExists(int id)
        { 
            return _appDbContext.Categories.Any(c=>c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _appDbContext.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _appDbContext.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _appDbContext.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _appDbContext.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _appDbContext.Owners.Where(o=>o.Id==ownerId).Select(c=>c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _appDbContext.Owners.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _appDbContext.SaveChanges();
            if(saved > 0)
                return true;
            return false;
            //return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _appDbContext.Update(country);
            return Save();
        }
    }
}
