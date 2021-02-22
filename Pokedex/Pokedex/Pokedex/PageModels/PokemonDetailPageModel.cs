using FreshMvvm;
using Pokedex.Models;
using Pokedex.Services;
using PropertyChanged;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Pokedex.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class PokemonDetailPageModel : FreshBasePageModel
    {
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

            if (initData is Pokemon pokemon)
            {
                if (pokemon != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Pokemon = pokemon;
                        Sprites = new ObservableCollection<string>(pokemon.Gallery);
                    });
                }
                else
                {
                    await CoreMethods.DisplayAlert("Error", "This Pokemon could not be fetched.", "Ok");
                }
            }
        }
        #endregion
    }
}
