namespace AdventOfCode.WinUI.Contracts;

public interface IApplicationStateService
{
    Task LoadApplicationStateAsync();
    Task SaveApplicationStateAsync();
}
