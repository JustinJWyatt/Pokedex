# Pokedex

This project utilizes the pokeapi.co API to fetch Pokemon data. This project uses FreshMVVM as the framework for navigating between pages. FreshMVVM uses a naming convention that maps the INotifyPropertyChanged conformant view model to the Xamarin page. All of the properties on the view model use INotifyPropertyChanged interface through Fody Weavers. Makes it simpler to integrate and not have to implement the interface each time. It also has a useful IOC container library. I set up a wrapper for the HttpClient that makes requests. One item that is incomplete is the ApplyFilters() method on the MainPageModel.

I also used a CustomRenderer for the checkbox on the filter page.

![Screenshots](https://github.com/JustinJWyatt/Pokedex/blob/main/Screenshots/Screenshots.png)

