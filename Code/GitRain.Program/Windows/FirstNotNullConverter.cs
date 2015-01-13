using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Cvte.GitRain.Windows
{
    public class FirstNotNullConverter : IMultiValueConverter
    {
        public static readonly FirstNotNullConverter Default = new FirstNotNullConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.FirstOrDefault(x =>
            {
                string s = x as string;
                return s == null ? !Equals(x, null) : !String.IsNullOrEmpty(s);
            });
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}