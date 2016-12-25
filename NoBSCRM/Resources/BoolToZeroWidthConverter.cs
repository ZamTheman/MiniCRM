using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace NoBSCRM.Resources
{
    public class BoolToZeroWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var input = (bool)value;
            ColumnDefinition def = new ColumnDefinition();
            def.Width = input ? new GridLength(1, GridUnitType.Star) : new GridLength(0, GridUnitType.Pixel);
            return def.Width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            int convertedValue = int.Parse(value.ToString().Substring(0,1));
            return convertedValue == 0 ? false : true;
        }
    }
}
