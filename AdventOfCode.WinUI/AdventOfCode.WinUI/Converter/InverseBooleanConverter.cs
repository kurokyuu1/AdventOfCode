using Microsoft.UI.Xaml.Data;

namespace AdventOfCode.WinUI.Converter;

public sealed class InverseBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }
        return value;
    }
}
