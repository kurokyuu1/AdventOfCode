using System.ComponentModel;
using AdventOfCode.WinUI.Strings;

namespace AdventOfCode.WinUI.Attributes;

public class LocalizedDescriptionAttribute : DescriptionAttribute
{
    private readonly string? _resourceKey;

    public LocalizedDescriptionAttribute(string? resourceKey)
    {
        _resourceKey = resourceKey;
    }

    public override string Description
    {
        get
        {
            if (_resourceKey is null)
            {
                return string.Empty;
            }

            var description = AppResources.GetLocalized(_resourceKey);
            return string.IsNullOrWhiteSpace(description) ? $"[[{_resourceKey}]]" : description;
        }
    }
}