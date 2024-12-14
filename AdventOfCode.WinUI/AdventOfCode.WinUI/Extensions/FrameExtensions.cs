using System.Reflection;
using Microsoft.UI.Xaml.Controls;

namespace AdventOfCode.WinUI.Extensions;

public static class FrameExtensions
{
    public static object? GetPageViewModel(this Frame? frame) =>
        frame
            ?.Content
            ?.GetType()
            .GetProperty("ViewModel", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
            ?.GetValue(frame.Content, null);
}