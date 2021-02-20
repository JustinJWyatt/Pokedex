using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Client
{
    public interface IPokeAPIClient
    {
        Task<T> SendRequestAsync<T>(Uri uri, HttpMethod method);
    }
}
