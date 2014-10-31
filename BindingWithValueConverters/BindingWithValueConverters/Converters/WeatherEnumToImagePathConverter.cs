using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BindingWithValueConverters.Converters
{
    public class WeatherEnumToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var wheather = (Weather)value;
            var path = string.Empty;
            switch (wheather)
            {
                case Weather.Sunny:
                    path = "Assets/sunny.png";
                    break;
                case Weather.Cloudy:
                    path = "Assets/cloudy.png";
                    break;
                case Weather.Rainy:
                    path = "Assets/rainy.png";
                    break;
                case Weather.Snowy:
                    path = "Assets/snowy.png";
                    break;
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
