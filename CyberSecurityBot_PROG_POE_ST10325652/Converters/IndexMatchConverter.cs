using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CyberSecurityBot_PROG_POE_ST10325652.Converters
{
    public class IndexMatchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int selectedIndex && parameter is string indexStr && int.TryParse(indexStr, out int index))
            {
                return selectedIndex == index;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter is string indexStr && int.TryParse(indexStr, out int index))
            {
                return index;
            }
            return Binding.DoNothing;
        }
    }
}
