using Windows.System;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Helper;
using AdventOfCode.WinUI.Utils;
using AdventOfCode.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace AdventOfCode.WinUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel { get; }
        public ShellPage(ShellViewModel viewModel)
        {
            ViewModel = viewModel;
            this.InitializeComponent();
            ViewModel.NavigationService.Frame = ContentFrame;
            ViewModel.NavigationViewService.Initialize(NavigationViewControl);
            
            App.MainWindow.ExtendsContentIntoTitleBar = true;
            App.MainWindow.SetTitleBar(AppTitleBar);
            App.MainWindow.Activated += MainWindow_Activated;

            AppTitleBarText.Text = "Advent of Code";
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            App.AppTitleBar = AppTitleBarText;
        }
        
        private void ShellPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            TitleBarHelper.UpdateTitleBar(RequestedTheme);

            KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
            KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = default)
        {
            var keyboardAccelerator = new KeyboardAccelerator
            {
                Key = key,
            };

            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }
            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

            return keyboardAccelerator;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            args.Handled = DiManager.GetService<INavigationService>().GoBack();
        }

        private void NavigationViewControl_OnDisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
        {
            AppTitleBar.Margin = new()
            {
                Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2:1),
                Top = AppTitleBar.Margin.Top,
                Right = AppTitleBar.Margin.Right,
                Bottom = AppTitleBar.Margin.Bottom,
            };
        }
    }
}
