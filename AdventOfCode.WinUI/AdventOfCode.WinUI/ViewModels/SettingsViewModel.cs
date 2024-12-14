using System.Reflection;
using Windows.ApplicationModel;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Helper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdventOfCode.WinUI.ViewModels;

public sealed partial class SettingsViewModel : ObservableRecipient
{
    private readonly ILogger<SettingsViewModel> _logger;
    private readonly IThemeSelectorService _themeSelectorService;

    [ObservableProperty]
    private ElementTheme _elementTheme;

    [ObservableProperty]
    private string _versionDescription;

    public SettingsViewModel(ILogger<SettingsViewModel> logger, IThemeSelectorService themeSelectorService)
    {
        _logger = logger;
        _themeSelectorService = themeSelectorService;

        ElementTheme = _themeSelectorService.Theme;
        VersionDescription = GetVersionDescription();
    }

    [RelayCommand]
    public async Task OnSwitchThemeAsync(ElementTheme theme)
    {
        if (ElementTheme != theme)
        {
            ElementTheme = theme;
            await _themeSelectorService.SetThemeAsync(theme);
        }
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMsix)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"AdventOfCode - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
