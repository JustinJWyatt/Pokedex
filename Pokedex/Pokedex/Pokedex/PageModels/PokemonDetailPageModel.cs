using FreshMvvm;
using Pokedex.Models;
using Pokedex.Services;
using PropertyChanged;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Pokedex.Utilities;

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

        #region Commands
        public Command CloseCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true, true);
        });
        #endregion

        #region Overrides
        public override async void Init(object initData)
        {
            base.Init(initData);

            if (initData is string url)
            {
                try
                {
                    var pokemon = await _pokemonService.GetPokemonAsync(url);

                    if (pokemon != null)
                    {
                        Pokemon = pokemon;
                        Pokemon.Name = Pokemon.Name.UppercaseFirst();
                        Sprites = new ObservableCollection<string>()
                        {
                            Pokemon.Sprites.FrontDefault,
                            Pokemon.Sprites.FrontShiny,
                            Pokemon.Sprites.FrontShinyFemale,
                            Pokemon.Sprites.BackDefault,
                            Pokemon.Sprites.BackShiny,
                            Pokemon.Sprites.BackFemale,
                            Pokemon.Sprites.BackShinyFemale
                        };
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Error", "This Pokemon could not be fetched.", "Ok");
                    }
                }
                catch (System.Exception)
                {
                    await CoreMethods.DisplayAlert("Error", "Could not retrieve data.", "Ok");

                }
            }
        }
        #endregion
    }
}
