
namespace AdventOfCode.WinUI.Contracts.Services;

public interface ILocalSettingsService
{
    Task<T?> ReadSettingsAsync<T>(string key);
    Task SaveSettingsAsync<T>(string key, T value);
}
