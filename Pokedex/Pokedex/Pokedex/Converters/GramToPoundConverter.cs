using System;
using System.Globalization;
using Xamarin.Forms;

namespace Pokedex.Converters
{
    public class GramToPoundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("{0} lbs", Math.Round(((int)value) * 0.2205d, 1));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
