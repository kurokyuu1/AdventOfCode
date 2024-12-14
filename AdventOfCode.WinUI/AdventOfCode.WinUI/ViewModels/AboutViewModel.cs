using CommunityToolkit.Mvvm.ComponentModel;

namespace AdventOfCode.WinUI.ViewModels;

public sealed partial class AboutViewModel : ObservableObject
{
    [ObservableProperty]
    private string _appVersion;

    [ObservableProperty]
    private string _dotnetVersion;

    [ObservableProperty]
    private string _aboutText;

    public AboutViewModel()
    {
        //var version = TwAppinfo.DotnetVersion.Split(',', StringSplitOptions.RemoveEmptyEntries);
        DotnetVersion = string.Empty; //TwAppinfo.Version;//$"{version[0].Replace("CoreApp", string.Empty)} {version[1].Replace("Version=v", string.Empty)}";
        AboutText = string
            .Empty; //string.Format(AppResources.AboutViewModel_AboutText, TwAppinfo.Version, DotnetVersion);
    }
}
