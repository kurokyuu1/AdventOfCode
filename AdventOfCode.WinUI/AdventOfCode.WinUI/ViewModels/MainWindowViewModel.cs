#region "Usings"

using System.Collections.ObjectModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using AdventOfCode.Core.Contracts;
using AdventOfCode.Riddles._2022.Models;
using AdventOfCode.Riddles._2023.Models;
using AdventOfCode.Riddles._2024.Models;
using AdventOfCode.WinUI.Contracts;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Models;
using AdventOfCode.WinUI.Strings;
using AdventOfCode.WinUI.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

#endregion

namespace AdventOfCode.WinUI.ViewModels;

public sealed partial class MainWindowViewModel : ObservableRecipient
{
    private readonly ILogger<MainWindowViewModel> _logger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
    {
        _logger = logger;
        Years = RiddlesByYear.Keys.ToArray();
        SelectedYear = Years[^1];
        BuildStaggeredLayoutForYear(SelectedYear);
    }

    [ObservableProperty]
    private int _selectedYear;

    [ObservableProperty]
    private ObservableCollection<RiddleItem> _riddles = [];

    [ObservableProperty]
    private int[] _years = [];

    private readonly static Dictionary<int, Dictionary<int, IAdventModule>> RiddlesByYear = new()
    {
        { 2022, Aoc2022Extensions.ModulesFor2022Dictionary },
        { 2023, Aoc2023Extensions.ModulesFor2023Dictionary },
        { 2024, Aoc2024Extensions.ModulesFor2024Dictionary },
    };

    
    private static void CopyToClipboard(string text)
    {
        var dataPackage = new DataPackage();
        dataPackage.SetText(text);
        Clipboard.SetContent(dataPackage);
    }

    [RelayCommand]
    private async Task OnRiddleItemClickedAsync(RiddleItem item)
    {
        if (item.Module is { })
        {
            await item.Module.RunAsync();
            item.Description =
                $"[Puzzle 1]: {item.Module.PuzzleResult1}{Environment.NewLine}[Puzzle 2]: {item.Module.PuzzleResult2}";
        }

        var dlg = new ContentDialog
        {
            Title = item.Title,
            Content = item.Description,
            CloseButtonText = AppResources.GetLocalized("Close"),
            PrimaryButtonText = AppResources.GetLocalized("Copy"),
            PrimaryButtonCommand = new RelayCommand(() => CopyToClipboard(item.Description)),
            XamlRoot = App.MainWindow.Content.XamlRoot,
        };
        await dlg.ShowAsync();
    }

    public void BuildStaggeredLayoutForYear(int year)
    {
        Riddles.Clear();
        var riddles = RiddlesByYear[year];

        var dayRanges = Enumerable.Range(1, 31).ToArray();

        foreach (var day in dayRanges)
        {
            //var red = (byte)Random.Shared.Next(0, 255);
            //var green = (byte)Random.Shared.Next(0, 255);
            //var blue = (byte)Random.Shared.Next(0, 255);
            var hue = Random.Shared.Next(0, 30);
            var saturation = Random.Shared.NextDouble() * .5 + .5;
            var lightness = Random.Shared.NextDouble() * .4 + .4;
            var adventColor = AdventColor.FromHsl(hue, saturation, lightness).GenerateColor();
            var height = Random.Shared.Next(200, 250);
            if (riddles.TryGetValue(day, out var module))
            {
                var item = new RiddleItem
                {
                    //Color = Color.FromArgb(0xff, red, green, blue),
                    Color = adventColor.ToColor(),
                    Year = year,
                    Day = day,
                    Height = height,
                    IsStarted = true,
                    Title = "Day " + day,
                    Index = Riddles.Count + 1,
                    Module = module,
                };
                Riddles.Add(item);
            }
            else
            {
                var item = new RiddleItem
                {
                    Color = Color.FromArgb(0xff, 0xa9, 0xa9, 0xa9),
                    Year = year,
                    Day = day,
                    Height = height,
                    IsStarted = false,
                    Title = "Day " + day,
                    Description = "Not yet solved",
                    Index = Riddles.Count + 1,
                };
                Riddles.Add(item);
            }
        }
    }
}
