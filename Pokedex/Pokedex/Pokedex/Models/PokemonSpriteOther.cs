using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public class PokemonSpriteOther
    {
        [JsonProperty("dream_world")]
        public PokemonSprite DreamWorld { get; set; }
        
        [JsonProperty("official-artwork")]
        public PokemonSprite OfficialArtwork { get; set; }
    }
}
