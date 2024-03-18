using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Converters
{
    internal class NameToFloatNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueString=value as string;
            if (valueString == null) return "";
            if (valueString.Contains("Battle-Scarred")) return "BS";
            if (valueString.Contains("Factory New")) return "FN";
            if (valueString.Contains("Minimal Wear")) return "MW";
            if (valueString.Contains("Well-Worn")) return "WW";
            if (valueString.Contains("Field-Tested")) return "FT";
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
