#region "Usings"

using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Helper;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

#endregion

namespace AdventOfCode.WinUI.Behaviors;

public sealed class NavigationViewHeaderBehavior : Behavior<NavigationView>
{
    #region "Constants"

    private static NavigationViewHeaderBehavior? _current;

    public static readonly DependencyProperty DefaultHeaderProperty =
        DependencyProperty.Register("DefaultHeader", typeof(object), typeof(NavigationViewHeaderBehavior),
            new(null, (d, e) => _current!.UpdateHeader()));

    public static readonly DependencyProperty HeaderContextProperty =
        DependencyProperty.RegisterAttached("HeaderContext", typeof(object), typeof(NavigationViewHeaderBehavior),
            new(null, (d, e) => _current!.UpdateHeader()));

    public static readonly DependencyProperty HeaderModeProperty =
        DependencyProperty.RegisterAttached("HeaderMode", typeof(bool), typeof(NavigationViewHeaderBehavior),
            new(NavigationViewHeaderMode.Always, (d, e) => _current!.UpdateHeader()));

    public static readonly DependencyProperty HeaderTemplateProperty =
        DependencyProperty.RegisterAttached("HeaderTemplate", typeof(DataTemplate),
            typeof(NavigationViewHeaderBehavior), new(null, (d, e) => _current!.UpdateHeaderTemplate()));

    #endregion

    #region "Variables"

    private Page? _currentPage;

    #endregion

    #region "Properties"

    public object DefaultHeader
    {
        get => GetValue(DefaultHeaderProperty);
        set => SetValue(DefaultHeaderProperty, value);
    }

    public DataTemplate? DefaultHeaderTemplate { get; set; }

    #endregion

    #region "Methods"

    public static object GetHeaderContext(Page item) => item.GetValue(HeaderContextProperty);

    public static NavigationViewHeaderMode GetHeaderMode(Page item) =>
        (NavigationViewHeaderMode)item.GetValue(HeaderModeProperty);

    public static DataTemplate GetHeaderTemplate(Page item) => (DataTemplate)item.GetValue(HeaderTemplateProperty);

    protected override void OnAttached()
    {
        base.OnAttached();

        var navigationService = DiManager.GetService<INavigationService>();
        navigationService.Navigated += OnNavigated;

        _current = this;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        var navigationService = DiManager.GetService<INavigationService>();
        navigationService.Navigated -= OnNavigated;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame { Content: Page page })
        {
            _currentPage = page;

            UpdateHeader();
            UpdateHeaderTemplate();
        }
    }

    public static void SetHeaderContext(Page item, object value) => item.SetValue(HeaderContextProperty, value);

    public static void SetHeaderMode(Page item, NavigationViewHeaderMode value) =>
        item.SetValue(HeaderModeProperty, value);

    public static void SetHeaderTemplate(Page item, DataTemplate value) => item.SetValue(HeaderTemplateProperty, value);

    private void UpdateHeader()
    {
        if (_currentPage == null)
        {
            return;
        }

        var headerMode = GetHeaderMode(_currentPage);
        if (headerMode == NavigationViewHeaderMode.Never)
        {
            AssociatedObject.Header = null;
            AssociatedObject.AlwaysShowHeader = false;
        }
        else
        {
            var headerFromPage = GetHeaderContext(_currentPage);
            AssociatedObject.Header = headerFromPage ?? DefaultHeader;

            AssociatedObject.AlwaysShowHeader = headerMode == NavigationViewHeaderMode.Always;
        }
    }

    private void UpdateHeaderTemplate()
    {
        if (_currentPage == null)
        {
            return;
        }

        var headerTemplate = GetHeaderTemplate(_currentPage);
        AssociatedObject.HeaderTemplate = headerTemplate ?? DefaultHeaderTemplate;
    }

    #endregion
}
