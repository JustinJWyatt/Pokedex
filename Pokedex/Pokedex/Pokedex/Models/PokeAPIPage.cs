using System.Collections.Generic;

namespace Pokedex.Models
{
    public class PokeAPIPage
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<PokeAPIPageResult> Results { get; set; }
    }
}
