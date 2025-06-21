using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Converters
{
    public class SelectedItemEventArgsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
          //  Shell.Current.DisplayAlert("Jo", $"Ich brauche den typ {value.GetType()}", "OK");
            if (value is SelectionChangedEventArgs args)
            {
                return args.CurrentSelection.FirstOrDefault();
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
