using Pokedex.Models;
using Pokedex.Services;
using System;
using FreshMvvm;
using System.Collections.ObjectModel;
using System.Text;
using Pokedex.Constants;
using Xamarin.Forms;
using PropertyChanged;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Pokedex.Utilities;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageModel : FreshBasePageModel
    {
        private readonly IPokemonService _pokemonService;
        private Pokemon _selectedPokemon;

        public MainPageModel(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        #region Properties
        public ObservableCollection<Pokemon> Pokemon { get; set; } = new ObservableCollection<Pokemon>();
        public PokeAPIPage PokeAPIPage { get; set; }
        public bool IsLoading { get; set; }
        public Pokemon SelectedPokemon
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
            await LoadPage(PokeAPIPage.Next);
        });

        public Command ShowPokemonDetail => new Command<Pokemon>((pokemon) =>
        {
            //TODO: Show Pokemon navigation
        });

        public Command PokemonSelectedCommand => new Command<Pokemon>((pokemon) =>
        {
            ShowPokemonDetail.Execute(pokemon);
        });
        #endregion

        #region Methods
        public async Task LoadPage(string uri)
        {
            IsLoading = true;

            PokeAPIPage = await _pokemonService.GetPokeAPIPage(uri);

            var uris = PokeAPIPage.Results.Select(result => result.Url);

            await LoadPokemonAsync(uris);
        }

        public async Task LoadPokemonAsync(IEnumerable<string> uris)
        {
            var request = uris.ToList().Select(_pokemonService.GetPokemon);

            //TODO: Cache details

            var response = await Task.WhenAll(request);

            Device.BeginInvokeOnMainThread(() =>
            {
                response.ToList().ForEach((pokemon) =>
                {
                    pokemon.Name = pokemon.Name.UppercaseFirst();
                    Pokemon.Add(pokemon);
                });

                IsLoading = false;
            });
        }
        #endregion

        #region Overrides
        public override async void Init(object initData)
        {
            base.Init(initData);

            if (PokeAPIPage == null)
            {
                await LoadPage(PokeAPI.BaseUrl);
            }
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }
        #endregion
    }
}
