using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ClientSamokat.Converters
{
    internal class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool _temp = (bool)value;
            if (!_temp)
                return Brushes.DarkGreen;
            else
                return Brushes.DarkRed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
