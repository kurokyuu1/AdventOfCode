using Microsoft.Windows.ApplicationModel.Resources;
using static Microsoft.Windows.ApplicationModel.Resources.ResourceLoader;

namespace AdventOfCode.WinUI.Strings;

public class AppResources
{
    private readonly static ResourceLoader _loader = new(GetDefaultResourceFilePath());

    public static string GetLocalized(string key) => _loader.GetString(key);
}
