using System;
using System.Globalization;
using Xamarin.Forms;

namespace RealEstate.Controls
{
    public class ObjectToStringMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object value = values[0] ?? string.Empty;
            var format = (string)values[1];
            string formattedValue;

            if (format == null)
                formattedValue = value.ToString();
            else if (format.StartsWith("{0:"))
                formattedValue = string.Format(format, value);
            else
                formattedValue = string.Format($"{{0:{format}}}", value);

            return formattedValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
