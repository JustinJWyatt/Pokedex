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
        private readonly ILocalRepositoryService _localRepositoryService;

        public PokemonService(IPokeAPIClient pokeAPIClient, 
                              ILocalRepositoryService localRepositoryService)
        {
            _pokeAPIClient = pokeAPIClient;
            _localRepositoryService = localRepositoryService;
        }

        public Task<PokeAPIPage> GetPokeAPIPageAsync(string uri)
        {
            return _pokeAPIClient.SendRequestAsync<PokeAPIPage>(new Uri(uri), HttpMethod.Get);
        }

        public Task<Pokemon> GetPokemonAsync(string uri)
        {
            return _pokeAPIClient.SendRequestAsync<Pokemon>(new Uri(uri), HttpMethod.Get);
        }
    }
}
