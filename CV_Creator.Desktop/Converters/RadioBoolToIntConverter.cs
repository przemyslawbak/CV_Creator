using System;
using System.Globalization;
using System.Windows.Data;

namespace CV_Creator.Desktop.Converters
{
    //credits: https://stackoverflow.com/a/1360425
    //optional: https://stackoverflow.com/a/24166629
    public class RadioBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int)value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
