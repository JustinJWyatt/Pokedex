using System.Collections.Generic;
using System.Collections.ObjectModel;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using Pokedex.ViewModels;
using System.Linq;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class PokemonTypeFilterPageModel : FreshBasePageModel
    {
        #region Properties
        public ObservableCollection<PokemonTypeViewModel> Types { get; set; }
        #endregion

        #region Commands
        public Command ApplyFiltersCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(Types.ToList(), true, true);
        });
        #endregion

        #region Overrides
        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is List<PokemonTypeViewModel> types)
            {
                Types = new ObservableCollection<PokemonTypeViewModel>(types);
            }
        }
        #endregion
    }
}
