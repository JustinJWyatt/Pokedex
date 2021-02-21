using Pokedex.Models;
using Pokedex.Services;
using FreshMvvm;
using System.Collections.ObjectModel;
using Pokedex.Constants;
using Xamarin.Forms;
using PropertyChanged;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Pokedex.Utilities;
using Pokedex.RepositoryModels;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageModel : FreshBasePageModel
    {
        #region Members
        private readonly IPokemonService _pokemonService;
        private readonly ILocalRepositoryService _localRepositoryService;
        private PokemonRepository _selectedPokemon;
        #endregion

        public MainPageModel(IPokemonService pokemonService, ILocalRepositoryService localRepositoryService)
        {
            _pokemonService = pokemonService;
            _localRepositoryService = localRepositoryService;
        }

        #region Properties
        public List<PokemonRepository> Pokemon { get; set; } = new List<PokemonRepository>();
        public ObservableCollection<PokemonRepository> FilteredResults { get; set; } = new ObservableCollection<PokemonRepository>();
        public ObservableCollection<PokemonType> TypeFilters { get; set; }
        public PokeAPIPageRepository PokeAPIPage { get; set; }
        public int PageNumber { get; set; }
        public bool IsLoading { get; set; }
        public PokemonRepository SelectedPokemon
        {
            get
            {
                return _selectedPokemon;
            }

            set
            {
                _selectedPokemon = value;
                if (value == null) return;
                PokemonSelectedCommand.Execute(_selectedPokemon);
                SelectedPokemon = null;
            }
        }
        #endregion

        #region Commands
        public Command LoadMoreCommand => new Command(async () =>
        {
            if (IsLoading)
            {
                return;
            }

            if (!string.IsNullOrEmpty(PokeAPIPage.Next))
            {
                IsLoading = true;

                await GetPokeAPIPageAsync(PokeAPIPage.Next);
            }
            else
            {
                //TODO: Let the collection view know there is no more to load
            }
        });

        public Command ShowPokemonDetail => new Command<Pokemon>((pokemon) =>
        {
            //TODO: Show Pokemon navigation
        });

        public Command PokemonSelectedCommand => new Command<PokemonRepository>((pokemon) =>
        {
            ShowPokemonDetail.Execute(pokemon);
        });

        public Command PokemonFavoriteCommand => new Command<Pokemon>((pokemon) =>
        {
            //TODO: Toggle favorite icon color
        });
        #endregion

        #region Methods
        public async Task SavePageAsync(PokeAPIPageRepository pokeAPIPage)
        {
            await _localRepositoryService.SavePokeAPIRepositoryAsync(pokeAPIPage);
        }

        public async Task GetPokeAPIPageAsync(string uri)
        {
            var pokeAPIPage = await _pokemonService.GetPokeAPIPageAsync(uri);

            if (pokeAPIPage != null)
            {
                PageNumber++;

                PokeAPIPage = new PokeAPIPageRepository
                {
                    Count = pokeAPIPage.Count,
                    Next = pokeAPIPage.Next,
                    Previous = pokeAPIPage.Previous,
                    PageNumber = PageNumber
                };

                var uris = pokeAPIPage.Results.Select(result => result.Url);

                await Task.WhenAll(SavePokemonAsync(uris), SavePageAsync(PokeAPIPage));
            }
        }

        public async Task SavePokemonAsync(IEnumerable<string> uris)
        {
            var request = uris.ToList().Select(_pokemonService.GetPokemonAsync);

            var response = await Task.WhenAll(request);

            var repositories = response.Select(pokemon => new PokemonRepository
            {
                PageNumber = PageNumber,
                PokemonId = pokemon.Id,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                Name = pokemon.Name.UppercaseFirst(),
                Image = pokemon.Sprites.Other.OfficialArtwork.FrontDefault
            });

            await _localRepositoryService.SavePokemonAsync(repositories);

            //TODO: Add exception handling

            Device.BeginInvokeOnMainThread(() =>
            {
                Pokemon.AddRange(repositories);
                FilteredResults = new ObservableCollection<PokemonRepository>(Pokemon);
                IsLoading = false;
            });
        }
        #endregion

        #region Overrides
        public override async void Init(object initData)
        {
            base.Init(initData);

            IsLoading = true;

            if (PokeAPIPage == null)
            {
                var pokeAPIPages = await _localRepositoryService.GetPokeAPIRepositoryAsync();

                if (pokeAPIPages.Count == 0)
                {
                    await GetPokeAPIPageAsync(PokeAPI.BaseUrl);
                }
                else
                {
                    PokeAPIPage = pokeAPIPages.Last();

                    PageNumber = PokeAPIPage.PageNumber;

                    for (int i = 1; i <= PageNumber; i++)
                    {
                        Pokemon.AddRange(await _localRepositoryService.GetPokemonAsync(i));
                    }

                    FilteredResults = new ObservableCollection<PokemonRepository>(Pokemon);

                    IsLoading = false;
                }
            }
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is List<PokemonType> typeFilters)
            {
                TypeFilters = new ObservableCollection<PokemonType>(typeFilters);

                //TODO: Filtered results from list of types
            }
        }
        #endregion
    }
}
