using Microsoft.UI.Xaml.Controls;

namespace AdventOfCode.WinUI.Navigation;

public sealed class NavigationHelper
{
    public static readonly DependencyProperty NavigateToProperty = DependencyProperty.RegisterAttached(
        "NavigateTo",
        typeof(string),
        typeof(NavigationHelper),
        new(null));

    public static string GetNavigateTo(NavigationViewItem item) =>
        (string)item.GetValue(NavigateToProperty);

    public static void SetNavigateTo(NavigationViewItem item, string value) =>
        item.SetValue(NavigateToProperty, value);
}
