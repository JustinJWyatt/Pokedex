using Newtonsoft.Json;

namespace Pokedex.Models
{
    public class PokemonSprite
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }

        [JsonProperty("other")]
        public PokemonSpriteOther Other { get; set; }
    }
}
