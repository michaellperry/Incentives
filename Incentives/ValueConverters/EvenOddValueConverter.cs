using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Incentives.ValueConverters
{
    public class EvenOddValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("Use an EvenOddValueConverter with a brush property.");

            int number = (int)value;

            if (number % 2 == 0)
                return App.Current.Resources["LightBackgroundBrush"];
            else
                return App.Current.Resources["LessLightBackgroundBrush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
