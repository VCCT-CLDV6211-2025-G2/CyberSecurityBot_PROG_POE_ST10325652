using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CyberSecurityBot_PROG_POE_ST10325652.Converters
{
    public class SenderToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sender = value as string ?? "";
            return Application.Current.Resources[sender == "User" ? "UserBubble" : "BotBubble"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}

