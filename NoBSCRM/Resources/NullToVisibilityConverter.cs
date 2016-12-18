using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NoBSCRM.Resources
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var input = value;
            return (input != null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new object();
        }
    }
}
