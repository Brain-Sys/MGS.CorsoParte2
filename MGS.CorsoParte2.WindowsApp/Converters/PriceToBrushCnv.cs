using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MGS.CorsoParte2.WindowsApp.Converters
{
    class PriceToBrushCnv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush result = Brushes.Transparent;
            double dbl = (double)value;
            string key = null;

            if (dbl >= 0 && dbl <= 500)
            {
                key = "DoorCheap";
            }
            else if (dbl > 500 && dbl <= 4000)
            {
                key = "DoorNormal";
            }
            else
            {
                key = "DoorExpensive";
            }

            result = Application.Current.Resources[key] as Brush;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}