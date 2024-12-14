using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.Win32;
using Windows.Win32.Foundation;
using AdventOfCode.WinUI.Extensions.ExternalEnums;
using Microsoft.UI;
using WinRT.Interop;

namespace AdventOfCode.WinUI.Utils;

internal sealed class TitleBarHelper
{
    public static void UpdateTitleBar(ElementTheme theme)
    {
        if (theme.IsDefaultTheme())
        {
            var uiSettings = new UISettings();
            var bg = uiSettings.GetColorValue(UIColorType.Background);
            
            theme = bg == Colors.White ? ElementTheme.Light : ElementTheme.Dark;
        }
        else
        {
            theme = Application.Current.RequestedTheme.ToElementTheme();
        }

        var buttonForegroundColor = theme.IsDarkTheme() ? Colors.White : Colors.Black;
        var buttonHoverBackgroundColor = theme.IsDarkTheme() ? Color.FromArgb(0x33, 0xFF, 0xFF, 0xFF) : Color.FromArgb(0x33, 0x00, 0x00, 0x00);
        var buttonPressedBackgroundColor = theme.IsDarkTheme() ? Color.FromArgb(0x66, 0xFF, 0xFF, 0xFF) : Color.FromArgb(0x66, 0x00, 0x00, 0x00);

        App.MainWindow.AppWindow.TitleBar.ButtonForegroundColor = buttonForegroundColor;
        App.MainWindow.AppWindow.TitleBar.ButtonHoverForegroundColor = buttonForegroundColor;
        App.MainWindow.AppWindow.TitleBar.ButtonHoverBackgroundColor = buttonHoverBackgroundColor;
        App.MainWindow.AppWindow.TitleBar.ButtonPressedBackgroundColor = buttonPressedBackgroundColor;

        App.MainWindow.AppWindow.TitleBar.BackgroundColor = Colors.Transparent;

        var windowHandle = WindowNative.GetWindowHandle(App.MainWindow);
        var hwnd = new HWND(windowHandle);

        if (hwnd == PInvoke.GetActiveWindow())
        {
            // Force the titlebar to redraw when the window is deactivated
            PInvoke.SendMessage(hwnd, PInvoke.WM_ACTIVATE, PInvoke.WA_INACTIVE, nint.Zero);
            PInvoke.SendMessage(hwnd, PInvoke.WM_ACTIVATE, PInvoke.WA_INACTIVE, nint.Zero);
        }
        else
        {
            // Force the titlebar to redraw when the window is activated
            PInvoke.SendMessage(hwnd, PInvoke.WM_ACTIVATE, PInvoke.WA_ACTIVE, nint.Zero);
            PInvoke.SendMessage(hwnd, PInvoke.WM_ACTIVATE, PInvoke.WA_ACTIVE, nint.Zero);
        }
    }

    public static void ApplySystemThemeToCaptionButtons()
    {
        if (App.AppTitleBar is FrameworkElement titlebar)
        {
            UpdateTitleBar(titlebar.ActualTheme);
        }
    }
}
