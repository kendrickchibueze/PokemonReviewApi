using PokemonReviewApi.Data;
using PokemonReviewApi.Dto;
using PokemonReviewApi.Interface;
using PokemonReviewApi.Model;

namespace PokemonReviewApi.Repository
{
    public class PokemonRepository:IPokemonRepository
    {

        private readonly ReviewDbContext _context;
        public PokemonRepository(ReviewDbContext context)
        {
            _context = context;
        }






        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }


        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }


        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }






        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemon.Any(p => p.Id == pokeId);
        }


        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

      

       

       

      
       

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate)
        {
            throw new NotImplementedException();
        }
    }
}
