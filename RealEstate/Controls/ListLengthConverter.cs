using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;

namespace RealEstate.Controls
{
    public class ListLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection list)
                return list.Count * GetItemLength(parameter);

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        float GetItemLength(object parameter)
        {
            if (parameter is float || parameter is double)
                return (float)parameter;
            else if (parameter is int)
                return (int)parameter;
            else if (parameter is string)
                return float.Parse((string)parameter);

            return 0;
        }
    }
}
