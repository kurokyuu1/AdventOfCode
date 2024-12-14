#region "Usings"

using System.Collections.ObjectModel;
using Windows.UI;
using AdventOfCode.Core.Contracts;
using AdventOfCode.Riddles._2022.Models;
using AdventOfCode.Riddles._2023.Models;
using AdventOfCode.Riddles._2024.Models;
using AdventOfCode.WinUI.Contracts;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

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

    public async Task ExecuteDaySolutionAsync(int year, int day)
    {
        if (RiddlesByYear.TryGetValue(year, out var riddles))
        {
            if (riddles.TryGetValue(day, out var module))
            {
                await module.RunAsync();
            }
        }
    }

    public void BuildStaggeredLayoutForYear(int year)
    {
        Riddles.Clear();
        var riddles = RiddlesByYear[year];

        var dayRanges = Enumerable.Range(1, 31).ToArray();

        foreach (var day in dayRanges)
        {
            var red = (byte)Random.Shared.Next(0, 255);
            var green = (byte)Random.Shared.Next(0, 255);
            var blue = (byte)Random.Shared.Next(0, 255);
            var height = Random.Shared.Next(200, 250);
            if (riddles.TryGetValue(day, out var module))
            {
                var item = new RiddleItem()
                {
                    Color = Color.FromArgb(0xff, red, green, blue),
                    Year = year,
                    Day = day,
                    Height = height,
                    IsStarted = true,
                    Title = "Day " + day,
                    Description = "Desc",
                    Index = Riddles.Count + 1,
                };
                Riddles.Add(item);
            }
            else
            {
                var item = new RiddleItem()
                {
                    Color = Color.FromArgb(0xff, 0xa9, 0xa9, 0xa9),
                    Year = year,
                    Day = day,
                    Height = height,
                    IsStarted = false,
                    Title = "Day " + day,
                    Description = "Desc",
                    Index = Riddles.Count + 1,
                };
                Riddles.Add(item);
            }
        }
    }
}
