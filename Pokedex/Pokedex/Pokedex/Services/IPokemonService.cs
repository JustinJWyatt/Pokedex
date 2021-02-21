using Pokedex.Models;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public interface IPokemonService
    {
        Task<PokeAPIPage> GetPokeAPIPageAsync(string uri);
        Task<Pokemon> GetPokemonAsync(string uri);
    }
}
