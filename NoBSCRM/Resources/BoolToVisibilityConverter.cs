using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NoBSCRM.Resources
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        //public object Convert(object value, Type targetType, object parameter, string language)
        //{
        //    bool input = (null == parameter) ? (bool)value : !((bool)value);
        //    return (input) ? Visibility.Visible : Visibility.Collapsed;
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, string language)
        //{
        //    throw new NotImplementedException();
        //}

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool input = (bool) value;
            return (input) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTimeOffset dto = (DateTimeOffset)value;
                return dto.DateTime;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
    }
}
