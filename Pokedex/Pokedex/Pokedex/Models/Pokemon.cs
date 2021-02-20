using System.Collections.Generic;

namespace Pokedex.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public PokemonSprite Sprites { get; set; }
        public List<PokemonTypeDefinition> Types { get; set; }
    }
}
