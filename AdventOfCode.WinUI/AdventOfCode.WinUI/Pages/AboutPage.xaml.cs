// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using AdventOfCode.WinUI.Helper;
using AdventOfCode.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AdventOfCode.WinUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        private AboutViewModel ViewModel { get; }
        public AboutPage()
        {
            ViewModel = DiManager.GetService<AboutViewModel>();
            this.InitializeComponent();
        }
    }
}
