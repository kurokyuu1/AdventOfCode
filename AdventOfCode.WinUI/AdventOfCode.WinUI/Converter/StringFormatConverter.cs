using Microsoft.UI.Xaml.Data;

namespace AdventOfCode.WinUI.Converter;

public sealed class StringFormatConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, string language)
    {
        if (value is null)
        {
            return default;
        }

        if(parameter is null)
        {
            return value;
        }

        try
        {
            return string.Format((string)parameter, value);
        }
        catch { }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
