using System;
using System.Globalization;
using Xamarin.Forms;

namespace Pokedex.Converters
{
    public class DecimetersToFeetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("{0} feet", Math.Round(((int)value) * 0.328084d, 1));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
