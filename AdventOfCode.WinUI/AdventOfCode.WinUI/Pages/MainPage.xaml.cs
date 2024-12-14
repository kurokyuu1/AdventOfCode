using AdventOfCode.WinUI.Helper;
using AdventOfCode.WinUI.Models;
using AdventOfCode.WinUI.Strings;
using AdventOfCode.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AdventOfCode.WinUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainWindowViewModel ViewModel { get; }
        public MainPage()
        {
            ViewModel = DiManager.GetService<MainWindowViewModel>();
            this.InitializeComponent();
            RedirectConsoleOutput();
        }

        private async void AdventOfCodeDays_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is RiddleItem item)
            {
                var dlg = new ContentDialog
                {
                    Title = item.Title,
                    Content = item.Description,
                    CloseButtonText = AppResources.GetLocalized("Close"),
                    XamlRoot = XamlRoot,
                };
                await ViewModel.ExecuteDaySolutionAsync(2024, item.Day);
                await dlg.ShowAsync();
            }
        }

        private void RedirectConsoleOutput()
        {
            var sw = new RiddleStringWriter();
            Console.SetOut(sw);

            sw.StringWritten += (_, e) =>
            {
                txtAdventOfCodeOutput.DispatcherQueue.TryEnqueue(() =>
                {
                    txtAdventOfCodeOutput.Text += e.Value;
                });
            };
        }
    }

    public sealed class RiddleStringWriter : StringWriter
    {
        public event EventHandler<TextWrittenEventArgs> StringWritten;

        public override void Write(string? value)
        {
            base.Write(value);
            OnStringWritten(value);
        }

        protected void OnStringWritten(string? value)
        {
            StringWritten?.Invoke(this, new TextWrittenEventArgs(value));
        }
    }

    public sealed class TextWrittenEventArgs : EventArgs
    {
        public string? Value { get; }

        public TextWrittenEventArgs(string? value)
        {
            Value = value;
        }
    }
    }
