using FreshMvvm;
using Pokedex.Client;
using Pokedex.PageModels;
using Pokedex.Services;
using Xamarin.Forms;

namespace Pokedex
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConfigureIOC();

            Init();
        }

        private void Init()
        {
            var page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            MainPage = new FreshNavigationContainer(page);
        }

        private void ConfigureIOC()
        {
            FreshIOC.Container.Register<IPokeAPIClient, PokeAPIClient>();
            FreshIOC.Container.Register<IPokemonService, PokemonService>();
            FreshIOC.Container.Register<ILocalRepositoryService, LocalRepositoryService>();
        }
    }
}
