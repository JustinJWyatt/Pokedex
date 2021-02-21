using Pokedex.RepositoryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public interface ILocalRepositoryService
    {
        Task<List<PokeAPIPageRepository>> GetPokeAPIRepositoryAsync();
        Task SavePokeAPIRepositoryAsync(PokeAPIPageRepository pokeAPIPage);
        Task SavePokemonAsync(IEnumerable<PokemonRepository> pokemon);
        Task SavePokemonAsync(PokemonRepository pokemon);
        Task<List<PokemonRepository>> GetPokemonAsync();
        Task<List<PokemonRepository>> GetPokemonAsync(int pageNumber);
    }
}
