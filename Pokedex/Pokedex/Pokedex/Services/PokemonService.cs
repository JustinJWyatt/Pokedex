using Pokedex.Client;
using Pokedex.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokeAPIClient _pokeAPIClient;

        public PokemonService(IPokeAPIClient pokeAPIClient)
        {
            _pokeAPIClient = pokeAPIClient;
        }

        public Task<PokeAPIPage> GetPokeAPIPage(string uri) => _pokeAPIClient.SendRequestAsync<PokeAPIPage>(new Uri(uri), HttpMethod.Get);

        public Task<Pokemon> GetPokemon(string uri) => _pokeAPIClient.SendRequestAsync<Pokemon>(new Uri(uri), HttpMethod.Get);

    }
}
