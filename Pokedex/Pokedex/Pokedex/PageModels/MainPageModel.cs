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

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageModel : FreshBasePageModel
    {
        private readonly IPokemonService _pokemonService;

        public MainPageModel(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        #region Properties
        public ObservableCollection<Pokemon> Pokemon { get; set; } = new ObservableCollection<Pokemon>();
        public PokeAPIPage PokeAPIPage { get; set; }
        #endregion

        #region Commands
        public Command LoadMoreCommand
        {
            get
            {
                return new Command(() =>
                {
                    //TODO: LoadMore();
                });
            }
        }

        public Command ShowPokemonDetail
        {
            get
            {
                return new Command<Pokemon>((pokemon) =>
                {
                    //TODO: Show Pokemon navigation
                });
            }
        }
        #endregion

        #region Methods
        public void LoadMore()
        {
            //TODO: Take next page and replace current page
        }

        public void LoadPokemon(IEnumerable<string> uris)
        {
            uris.ToList().ForEach(async (uri) =>
            {
                //TODO: Query cache for uri
                var pokemon = await _pokemonService.GetPokemon(uri);

                Pokemon.Add(pokemon);

                //TODO: Add details to cache
            });
        }
        #endregion

        #region Overrides
        public override async void Init(object initData)
        {
            base.Init(initData);

            if (PokeAPIPage == null)
            {
                var page = await _pokemonService.GetPokeAPIPage(PokeAPI.BaseUrl);

                PokeAPIPage = page;

                var uris = PokeAPIPage.Results.Select(result => result.Url);

                LoadPokemon(uris);
            }
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }
        #endregion
    }
}
