using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Xaml.Chat.Client.Helpers
{
    public class DateSimplifierConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
                 object parameter, System.Globalization.CultureInfo culture)
        {

            DateTime date = (DateTime)values[0];
            var returnHour = date.ToShortTimeString();
          //  var hour = date.Hour;

            return returnHour;

           
        }
        public object[] ConvertBack(object value, Type[] targetTypes,
               object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}
