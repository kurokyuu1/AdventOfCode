using System.Diagnostics.CodeAnalysis;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Navigation;
using AdventOfCode.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace AdventOfCode.WinUI.Services.Navigation;

public sealed class NavigationViewService : INavigationViewService
{
    private readonly ILogger<NavigationViewService> _logger;
    private readonly INavigationService _navigationService;
    private readonly IPageService _pageService;
    public IList<object>? MenuItems => _navigationView?.MenuItems;
    public object? SettingsItem => _navigationView?.SettingsItem;
    private NavigationView? _navigationView;

    public NavigationViewService(ILogger<NavigationViewService> logger, INavigationService navigationService,
        IPageService pageService)
    {
        _logger = logger;
        _navigationService = navigationService;
        _pageService = pageService;
    }

    [MemberNotNull(nameof(_navigationView))]
    public void Initialize(NavigationView navigationView)
    {
        _navigationView = navigationView;
        _navigationView.BackRequested += OnBackRequested;
        _navigationView.ItemInvoked += OnItemInvoked;
    }

    public void UnregisterEvents()
    {
        if (_navigationView is { })
        {
            _navigationView.BackRequested -= OnBackRequested;
            _navigationView.ItemInvoked -= OnItemInvoked;
        }
    }

    public NavigationViewItem? GetSelectedItem(Type pageType)
        => _navigationView is { }
            ? GetSelectedItem(_navigationView.MenuItems, pageType) ??
              GetSelectedItem(_navigationView.FooterMenuItems, pageType)
            : default;

    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => _navigationService.GoBack();

    private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
        }
        else if (args.InvokedItemContainer is NavigationViewItem selectedItem)
        {
            if (selectedItem.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
            {
                _navigationService.NavigateTo(pageKey);
            }
        }
    }

    private NavigationViewItem? GetSelectedItem(IEnumerable<object> menuItems, Type pageType)
    {
        foreach (var menuItem in menuItems.OfType<NavigationViewItem>())
        {
            if (IsMenuItemForPageType(menuItem, pageType))
            {
                return menuItem;
            }

            if (GetSelectedItem(menuItem.MenuItems, pageType) is { } selectedChild)
            {
                return selectedChild;
            }
        }

        return null;
    }

    private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
    {
        if (menuItem.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
        {
            return _pageService.GetPageType(pageKey) == sourcePageType;
        }

        return false;
    }
}