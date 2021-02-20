using Pokedex.Models;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public interface IPokemonService
    {
        Task<PokeAPIPage> GetPokeAPIPage(string uri);
        Task<Pokemon> GetPokemon(string uri);
    }
}
