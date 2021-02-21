using Pokedex.Models;
using SQLite;

namespace Pokedex.RepositoryModels
{
    public class PokemonRepository
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PokemonId { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Image { get; set; }
    }
}
