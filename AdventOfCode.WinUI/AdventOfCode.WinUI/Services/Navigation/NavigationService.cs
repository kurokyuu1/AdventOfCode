using System.Diagnostics.CodeAnalysis;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Extensions;
using Microsoft.UI.Xaml.Controls;

namespace AdventOfCode.WinUI.Services.Navigation;

public sealed class NavigationService : INavigationService
{
    private readonly ILogger<NavigationService> _logger;
    private readonly IPageService _pageService;
    private object? _lastParameterUsed;
    private Frame? _frame;
    public event NavigatedEventHandler? Navigated;

    public NavigationService(ILogger<NavigationService> logger, IPageService pageService)
    {
        _logger = logger;
        _pageService = pageService;
    }

    public Frame? Frame
    {
        get
        {
            if (_frame is null)
            {
                _frame = App.MainWindow.Content as Frame;
                RegisterFrameEvents();
            }

            return _frame;
        }
        set
        {
            UnregisterFrameEvents();
            _frame = value;
            RegisterFrameEvents();
        }
    }

    [MemberNotNullWhen(true, nameof(Frame), nameof(_frame))]
    public bool CanGoBack => Frame is { CanGoBack: true };

    private void RegisterFrameEvents()
    {
        if (_frame is null)
        {
            return;
        }

        _logger.LogInformation("Registering frame OnNavigated event!");
        _frame.Navigated += OnNavigated;
    }

    private void UnregisterFrameEvents()
    {
        if (_frame is null)
        {
            return;
        }

        _logger.LogInformation("Unregistering frame OnNavigated event!");
        _frame.Navigated -= OnNavigated;
    }

    public bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false)
    {
        try
        {
            var pageType = _pageService.GetPageType(pageKey);

            if (pageType is not { })
            {
                _logger.LogError("Failed to get page type for {PageKey}", pageKey);
                return false;
            }

            if (_frame is null || (_frame.Content?.GetType() == pageType &&
                                   (parameter == null || parameter.Equals(_lastParameterUsed))))
            {
                _logger.LogInformation("Frame is null or content is the same type as the page type!");
                return false;
            }
            _logger.LogInformation("Navigating to {PageKey} and found {PageType}", pageKey, pageType.FullName);
            _frame.Tag = clearNavigation;
            var viewModelBefore = _frame.GetPageViewModel();
            var navigated = _frame.Navigate(pageType, parameter);
            if (navigated)
            {
                _lastParameterUsed = parameter;
                if (viewModelBefore is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }
            }
            else
            {
                _logger.LogError("Failed to navigate to {PageType}", pageType);
            }

            return navigated;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to navigate to {PageKey}", pageKey);
            return false;
        }
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is not Frame frame)
        {
            _logger.LogError("sender is not from type Frame");
            return;
        }

        var clearNavigation = (bool)frame.Tag;
        if (clearNavigation)
        {
            frame.BackStack.Clear();
        }

        if (frame.GetPageViewModel() is INavigationAware navigationAware)
        {
            navigationAware.OnNavigatedTo(e.Parameter);
        }

        Navigated?.Invoke(sender, e);
    }

    public bool GoBack()
    {
        if (!CanGoBack)
        {
            return false;
        }

        var viewModelBeforeNavigation = _frame.GetPageViewModel();
        _frame!.GoBack();

        if (viewModelBeforeNavigation is INavigationAware navigationAware)
        {
            navigationAware.OnNavigatedFrom();
        }

        return true;
    }
}
