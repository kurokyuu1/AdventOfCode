using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AdventOfCode.WinUI.ViewModels;

public sealed partial class ShellViewModel : ObservableRecipient
{
    private readonly ILogger<ShellViewModel> _logger;
    public INavigationService NavigationService { get; }
    public INavigationViewService NavigationViewService { get; }

    [ObservableProperty]
    private bool _isBackEnabled;

    [ObservableProperty]
    private object? _selected;

    public ShellViewModel(ILogger<ShellViewModel> logger, INavigationService navigationService, INavigationViewService navigationViewService)
    {
        _logger = logger;
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem is { })
        {
            Selected = selectedItem;
        }

        //Selected = e.SourcePageType == typeof(SettingsPage)
        //    ? NavigationViewService.SettingsItem
        //    : NavigationViewService.GetSelectedItem(e.SourcePageType);
    }
}
