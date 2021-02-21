using SQLite;
using System;
using Pokedex.Constants;
using System.Threading.Tasks;
using Pokedex.RepositoryModels;
using System.Collections.Generic;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class LocalRepositoryService : ILocalRepositoryService
    {
        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            var flags = SQLiteOpenFlags.ReadWrite |
                        SQLiteOpenFlags.Create |
                        SQLiteOpenFlags.SharedCache;

            return new SQLiteAsyncConnection(LocalRepository.DatabasePath, flags);
        });

        private static SQLiteAsyncConnection Database => lazyInitializer.Value;

        public LocalRepositoryService()
        {
            InitializeAsync().ConfigureAwait(false);
        }

        private async Task InitializeAsync()
        {
            await Task.WhenAll(Database.CreateTableAsync<PokeAPIPageRepository>(), Database.CreateTableAsync<PokemonRepository>());
        }

        public async Task<List<PokeAPIPageRepository>> GetPokeAPIRepositoryAsync()
        {
            return await Database.Table<PokeAPIPageRepository>().ToListAsync();
        }

        public async Task SavePokeAPIRepositoryAsync(PokeAPIPageRepository pokeAPIPage)
        {
            await Database.InsertAsync(pokeAPIPage);
        }

        public async Task SavePokemonAsync(IEnumerable<PokemonRepository> pokemon)
        {
            await Database.InsertAllAsync(pokemon);
        }

        public async Task<List<PokemonRepository>> GetPokemonAsync()
        {
            return await Database.Table<PokemonRepository>().ToListAsync();
        }

        public async Task<List<PokemonRepository>> GetPokemonAsync(int pageNumber)
        {
            return await Database.Table<PokemonRepository>().Where(x => x.PageNumber == pageNumber).ToListAsync();
        }
    }
}
