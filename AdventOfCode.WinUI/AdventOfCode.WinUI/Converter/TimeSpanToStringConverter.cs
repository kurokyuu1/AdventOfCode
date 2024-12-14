using Microsoft.UI.Xaml.Data;

namespace AdventOfCode.WinUI.Converter;

public sealed class TimeSpanToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is TimeSpan timeSpan)
        {
            return timeSpan.ToString("g");
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is string stringValue)
        {
            if (TimeSpan.TryParse(stringValue, out var timeSpan))
            {
                return timeSpan;
            }
        }

        return TimeSpan.Zero;
    }
}
