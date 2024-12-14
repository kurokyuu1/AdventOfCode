using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Pages;
using AdventOfCode.WinUI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace AdventOfCode.WinUI.Services.Navigation;

public sealed class PageService : IPageService
{
    private readonly ILogger<PageService> _logger;
    private readonly Dictionary<string, Type> _pages = [];

    public PageService(ILogger<PageService> logger)
    {
        _logger = logger;
        Configure<MainWindowViewModel, MainPage>();
        Configure<SettingsViewModel, SettingsPage>();
        Configure<AboutViewModel, AboutPage>();
        Configure<ShellViewModel, ShellPage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                _logger.LogWarning("No such page: {Key}. Did you forget to call PageService.Configure?", key);
                throw new ArgumentException($"No such page: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<TViewModel, TView>()
        where TViewModel : ObservableObject
        where TView : Page
    {
        lock (_pages)
        {
            var key = typeof(TViewModel).FullName!;
            if (_pages.ContainsKey(key))
            {
                _logger.LogWarning("The key {Key} is already configured in PageService", key);
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(TView);
            if (_pages.ContainsValue(type))
            {
                _logger.LogWarning("The type {Type} is already configured in PageService", type);
                throw new ArgumentException($"The type {type} is already configured in PageService");
            }

            _pages.Add(key, type);
        }
    }
}
