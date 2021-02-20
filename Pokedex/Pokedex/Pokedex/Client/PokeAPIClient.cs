using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Client
{
    public class PokeAPIClient : IPokeAPIClient
    {
        public async Task<T> SendRequestAsync<T>(Uri uri, HttpMethod method)
        {
            var httpClient = new HttpClient();

            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.RequestUri = uri;
                request.Method = method;

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
                {
                    var contentAsString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(contentAsString);
                    }
                }
            }

            return default(T);
        }
    }
}
