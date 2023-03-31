using AutoMapper;
using PokeReview.Dto;
using PokeReview.Models;

namespace PokeReview.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();//get
            CreateMap<PokemonDto, Pokemon>();//post
            CreateMap<Category, CategoryDto>();//get
            CreateMap<CategoryDto, Category>();//post
            CreateMap<Country, CountryDto>();//get
            CreateMap<CountryDto, Country>();//post
            CreateMap<Owner, OwnerDto>();//get
            CreateMap<OwnerDto, Owner>();//post
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
