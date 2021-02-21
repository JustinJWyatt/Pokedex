using SQLite;

namespace Pokedex.RepositoryModels
{
    public class PokeAPIPageRepository
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
    }
}
