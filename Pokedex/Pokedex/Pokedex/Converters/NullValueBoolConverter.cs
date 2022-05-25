using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Converters
{
    public class NullValueBoolConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enums.SortOrder?)
            {
                return ((Enums.SortOrder?)value).HasValue;
            }

            return !((Enums.SortOrder?)value).HasValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
