﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonDetailPage : ContentPage
    {
        public PokemonDetailPage()
        {
            InitializeComponent();
        }
    }
}