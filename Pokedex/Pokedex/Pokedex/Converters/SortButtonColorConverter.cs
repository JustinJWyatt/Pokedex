using Pokedex.Enums;
using Pokedex.Models;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Pokedex.Converters
{
    public class SortButtonColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ColorCollection brushes = (ColorCollection)parameter;

            if (value != null && value is SortOrder sortOrder && sortOrder == SortOrder.Ascending)
            {
                return brushes.ToList().FirstOrDefault(x => x.Key == "Ascending");
            }

            return brushes.ToList().FirstOrDefault(x => x.Key == "Descending");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
