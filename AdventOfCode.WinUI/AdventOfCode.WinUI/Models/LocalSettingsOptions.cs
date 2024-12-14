using Windows.UI;

namespace AdventOfCode.WinUI.Models;

public sealed class LocalSettingsOptions
{
    public string? ApplicationDataFolder { get; set; }
    public string? SettingsFileName { get; set; }
}


public sealed class RiddleItem
{
    public required int Index { get; set; }
    public required int Year { get; set; }
    public required int Day { get; set; }
    public required int Height { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool SolvedPartOne { get; set; }
    public bool SolvedPartTwo { get; set; }
    public bool FullySolved => SolvedPartOne && SolvedPartTwo;
    public required bool IsStarted { get; set; }
    public required Color Color { get; set; }
    public Task Action { get; set; } = Task.CompletedTask;
}