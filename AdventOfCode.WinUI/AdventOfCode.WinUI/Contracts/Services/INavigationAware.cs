namespace AdventOfCode.WinUI.Contracts.Services;

public interface INavigationAware
{
    void OnNavigatedTo(object parameter);

    void OnNavigatedFrom();
}
