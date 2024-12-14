using Microsoft.UI.Xaml.Data;

namespace AdventOfCode.WinUI.Converter;

public sealed class DateToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("g");
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is string stringValue)
        {
            if (DateTimeOffset.TryParse(stringValue, out var dateTimeOffset))
            {
                return dateTimeOffset;
            }
        }

        return DateTimeOffset.Now;
    }
}