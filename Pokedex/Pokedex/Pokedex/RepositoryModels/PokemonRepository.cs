using Pokedex.Models;
using SQLite;
using System;

namespace Pokedex.RepositoryModels
{
    public class PokemonRepository : IComparable<PokemonRepository>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PokemonId { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public bool Favorite { get; set; }
        public string Types { get; set; }

        public int CompareTo(PokemonRepository other) => Name.CompareTo(other.Name);
    }
}
