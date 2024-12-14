namespace AdventOfCode.WinUI.Extensions.ExternalEnums;

public static partial class ApplicationThemeExtensions
{
    public static ElementTheme ToElementTheme(this ApplicationTheme theme) =>
        theme switch
        {
            ApplicationTheme.Light => ElementTheme.Light,
            ApplicationTheme.Dark => ElementTheme.Dark,
            var _ => ElementTheme.Default,
        };

    public static bool IsDarkTheme(this ApplicationTheme theme) => theme == ApplicationTheme.Dark;
    public static bool IsLightTheme(this ApplicationTheme theme) => theme == ApplicationTheme.Light;
}

public static partial class ElementThemeExtensions
{
    public static ApplicationTheme ToApplicationTheme(this ElementTheme theme) =>
        theme switch
        {
            ElementTheme.Light => ApplicationTheme.Light,
            ElementTheme.Dark => ApplicationTheme.Dark,
            var _ => ApplicationTheme.Light,
        };

    public static bool IsDarkTheme(this ElementTheme theme) => theme == ElementTheme.Dark;
    public static bool IsLightTheme(this ElementTheme theme) => theme == ElementTheme.Light;
    public static bool IsDefaultTheme(this ElementTheme theme) => theme == ElementTheme.Default;
}