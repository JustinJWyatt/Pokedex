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
using Pokedex.ViewModels;
using Pokedex.Enums;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageModel : FreshBasePageModel
    {
        #region Members
        private readonly IPokemonService _pokemonService;
        private readonly ILocalRepositoryService _localRepositoryService;
        private PokemonRepository _selectedPokemon;
        private List<PokemonRepository> _filteredResults;
        #endregion

        public MainPageModel(IPokemonService pokemonService, ILocalRepositoryService localRepositoryService)
        {
            _pokemonService = pokemonService;
            _localRepositoryService = localRepositoryService;
        }

        #region Properties
        public List<PokemonRepository> Pokemon { get; set; } = new List<PokemonRepository>();
        public ObservableCollection<PokemonRepository> FilteredResults { get; set; } = new ObservableCollection<PokemonRepository>();
        public List<PokemonTypeViewModel> Types { get; set; } = new List<PokemonTypeViewModel>();
        public PokeAPIPageRepository PokeAPIPage { get; set; }
        public int PageNumber { get; set; }
        public bool IsLoading { get; set; }
        public SortOrder? SortOrder { get; set; } = Enums.SortOrder.Ascending;
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
            if (!IsLoading && !string.IsNullOrEmpty(PokeAPIPage.Next))
            {
                IsLoading = !IsLoading;

                try
                {
                    await GetPokeAPIPageAsync(PokeAPIPage.Next);
                }
                catch (System.Exception)
                {
                    IsLoading = !IsLoading;

                    await CoreMethods.DisplayAlert("Error", "Could not retrieve data", "Ok");
                }
            }
        });

        public Command ShowPokemonDetailCommand => new Command<string>(async (url) =>
        {
            var result = await Task.WhenAll(_pokemonService.GetPokemonAsync(url));
            var pokemon = result[0]; //TODO: Optimize async loading
            pokemon.Name = pokemon.Name.UppercaseFirst();
            pokemon.Gallery = new List<string>()
            {
                pokemon.Sprites.FrontDefault,
                pokemon.Sprites.FrontShiny,
                pokemon.Sprites.FrontShinyFemale,
                pokemon.Sprites.BackDefault,
                pokemon.Sprites.BackShiny,
                pokemon.Sprites.BackFemale,
                pokemon.Sprites.BackShinyFemale

                //TODO: Add other sprites

            }.Where(sprite => !string.IsNullOrEmpty(sprite)).ToList();

            await CoreMethods.PushPageModel<PokemonDetailPageModel>(pokemon, true, true);
        });

        public Command PokemonSelectedCommand => new Command<PokemonRepository>((pokemon) =>
        {
            ShowPokemonDetailCommand.Execute(pokemon.Url);
        });

        public Command PokemonFavoriteCommand => new Command<PokemonRepository>((pokemon) =>
        {
            //TODO: Toggle favorite icon color
        });

        public Command ShowImageCommand => new Command<string>((url) =>
        {
            //TODO: Show Image full screen
        });

        public Command ShowPokemonFilterCommand => new Command(async () =>
        {
            if (Pokemon.Any())
            {
                await CoreMethods.PushPageModel<PokemonTypeFilterPageModel>(Types, true, true);
            }
        });

        public Command SortCommand => new Command(SortCommandHandler);
        #endregion

        #region Methods
        private void ApplyFilters()
        {
            FilteredResults = new ObservableCollection<PokemonRepository>();

            if (Types.Any() && Types.Any(type => type.Checked))
            {
                var results = new List<PokemonRepository>();
                var filteredTypes = Types.Where(type => type.Checked).Select(type => type.Name.ToLower());
                //var results = Pokemon.Where(pokemon => filteredTypes.Intersect(pokemon.Types.ToList()).Any()).ToList();
                Pokemon.ForEach((pokemon) =>
                {
                    var types = pokemon.Types.Split(new char[] { ',' }).Select(type => type.ToLower());
                    if (filteredTypes.Intersect(types).Any())
                    {
                        FilteredResults.Add(pokemon);
                    }
                });
            }
            else
            {
                FilteredResults = new ObservableCollection<PokemonRepository>(Pokemon);
            }

            SortOrder = null;
        }

        //TODO: Incomplete code
        private void SortCommandHandler()
        {
            var sortedResults = FilteredResults.ToList();

            if (!SortOrder.HasValue) SortOrder = Enums.SortOrder.Ascending;

            if (SortOrder.Value == Enums.SortOrder.Ascending)
            {
                SortOrder = Enums.SortOrder.Descending;

                sortedResults.Sort((a, b) => b.CompareTo(a));
            }
            else
            {
                SortOrder = Enums.SortOrder.Ascending;

                _filteredResults.OrderBy(x => x.Name);
            }

            FilteredResults = new ObservableCollection<PokemonRepository>(sortedResults);
        }

        public async Task SavePageAsync(PokeAPIPageRepository pokeAPIPage)
        {
            await _localRepositoryService.SavePokeAPIRepositoryAsync(pokeAPIPage);
        }

        public async Task GetPokeAPIPageAsync(string uri)
        {
            var pokeAPIPage = await _pokemonService.GetPokeAPIPageAsync(uri);

            if (pokeAPIPage != null)
            {
                SortOrder = null;

                PageNumber++;

                PokeAPIPage = new PokeAPIPageRepository
                {
                    Count = pokeAPIPage.Count,
                    Next = pokeAPIPage.Next,
                    Previous = pokeAPIPage.Previous,
                    PageNumber = PageNumber
                };

                await Task.WhenAll(SavePokemonAsync(pokeAPIPage.Results), SavePageAsync(PokeAPIPage));
            }
        }

        public async Task SavePokemonAsync(IEnumerable<PokeAPIPageResult> results)
        {
            var request = results.Select(result => result.Url).ToList()
                                 .Select(_pokemonService.GetPokemonAsync);

            var response = await Task.WhenAll(request);

            var repositories = response.Select(pokemon => new PokemonRepository
            {
                PageNumber = PageNumber,
                PokemonId = pokemon.Id,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                Name = pokemon.Name.UppercaseFirst(),
                Image = pokemon.Sprites.Other.OfficialArtwork.FrontDefault,
                Url = results.FirstOrDefault(result => result.Name == pokemon.Name).Url,
                Types = string.Join(",", pokemon.Types.Select(type => type.Type.Name).ToArray())
            });

            await _localRepositoryService.SavePokemonAsync(repositories);

            Device.BeginInvokeOnMainThread(() =>
            {
                Pokemon.AddRange(repositories);
                ApplyFilters(); //Don't sort. Revert to default
                IsLoading = false;
            });
        }

        public async Task GetPokemonTypesAsync()
        {
            var pokeAPIPage = await _pokemonService.GetPokeAPIPageAsync(PokeAPI.Types);
            Types = pokeAPIPage.Results.Select(result => new PokemonTypeViewModel
            {
                Name = result.Name.UppercaseFirst(),
                Checked = false

            }).ToList();
        }
        #endregion

        #region Overrides
        public override async void Init(object initData)
        {
            base.Init(initData);

            GetPokemonTypesAsync().FireAndForget();

            if (PokeAPIPage == null)
            {
                try
                {
                    IsLoading = true;

                    var pokeAPIPages = await _localRepositoryService.GetPokeAPIRepositoryAsync();

                    if (pokeAPIPages.Count == 0)
                    {
                        await GetPokeAPIPageAsync(PokeAPI.Pokemon);
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

                        SortCommandHandler();

                        IsLoading = false;
                    }
                }
                catch (System.Exception)
                {
                    IsLoading = false;

                    await CoreMethods.DisplayAlert("Error", "Could not retrieve data", "Ok");
                }
            }
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is List<PokemonTypeViewModel> typeFilters)
            {
                Types = typeFilters;
                ApplyFilters();
            }
        }
        #endregion
    }
}
