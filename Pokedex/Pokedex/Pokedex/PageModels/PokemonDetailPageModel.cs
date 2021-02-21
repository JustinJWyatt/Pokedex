using FreshMvvm;
using Pokedex.Models;
using Pokedex.Services;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class PokemonDetailPageModel : FreshBasePageModel
    {
        #region Members
        private readonly IPokemonService _pokemonService;
        #endregion

        public PokemonDetailPageModel(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        #region Properties
        public Pokemon Pokemon { get; set; }
        public ObservableCollection<string> Sprites { get; set; }
        #endregion

        #region Overrides
        public override async void Init(object initData)
        {
            base.Init(initData);

            if (initData is string url)
            {
                var pokemon = await _pokemonService.GetPokemonAsync(url);

                if (pokemon != null)
                {
                    Pokemon = pokemon;
                }
                else
                {
                    //TODO: Let the user know something went wrong
                }
            }
        }
        #endregion
    }
}
