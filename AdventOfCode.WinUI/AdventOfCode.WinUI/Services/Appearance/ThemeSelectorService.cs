#region "Usings"

using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Utils;

#endregion

namespace AdventOfCode.WinUI.Services.Appearance;

public sealed class ThemeSelectorService : IThemeSelectorService
{
    #region "Constants"

    private const string SettingsKey = "AppBackgroundRequestedTheme";

    #endregion

    #region "Variables"

    private readonly ILocalSettingsService _localSettingsService;

    #endregion

    #region "Properties"

    public ElementTheme Theme { get; set; } = ElementTheme.Default;

    #endregion

    #region "Constructor"

    public ThemeSelectorService(ILogger<ThemeSelectorService> logger, ILocalSettingsService localSettingsService) =>
        _localSettingsService = localSettingsService;

    #endregion

    #region "Interface Methods"

    public async Task InitializeAsync()
    {
        Theme = await LoadThemeFromSettingsAsync();
        await Task.CompletedTask;
    }

    public Task SetThemeAsync(ElementTheme theme)
    {
        Theme = theme;
        return Task.WhenAll([SetRequestedThemeAsync(), SaveThemeInSettingsAsync(theme)]);
    }

    public Task SetRequestedThemeAsync()
    {
        if (App.MainWindow.Content is FrameworkElement rootElement)
        {
            rootElement.RequestedTheme = Theme;
            TitleBarHelper.UpdateTitleBar(Theme);
        }

        return Task.CompletedTask;
    }

    #endregion

    #region "Methods"

    private async Task<ElementTheme> LoadThemeFromSettingsAsync()
    {
        var themeName = await _localSettingsService.ReadSettingsAsync<string>(SettingsKey);

        return Enum.TryParse(themeName, out ElementTheme cacheTheme)
            ? cacheTheme
            : ElementTheme.Default;
    }

    private Task SaveThemeInSettingsAsync(ElementTheme theme)
        => _localSettingsService.SaveSettingsAsync(SettingsKey, theme.ToString());

    #endregion
}
