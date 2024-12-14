using AdventOfCode.WinUI.Contracts;
using AdventOfCode.WinUI.Contracts.Services;

namespace AdventOfCode.WinUI.Services;
public sealed class ApplicationState
{
    public ElementTheme Theme { get; set; }
}
public sealed class ApplicationStateService : IApplicationStateService
{
    private readonly ILogger<ApplicationStateService> _logger;
    private readonly ILocalSettingsService _localSettingsService;
    private static ApplicationState? _applicationState;

    public ApplicationState? ApplicationState => _applicationState;

    public const string ApplicationStateSettingsKey = "ApplicationState";

    public ApplicationStateService(ILogger<ApplicationStateService> logger, ILocalSettingsService localSettingsService)
    {
        _logger = logger;
        _localSettingsService = localSettingsService;
    }

    public async Task LoadApplicationStateAsync()
    {
        _logger.LogInformation("Loading application state");
        _applicationState = await _localSettingsService.ReadSettingsAsync<ApplicationState>(ApplicationStateSettingsKey);
    }

    public Task SaveApplicationStateAsync()
    {
        _logger.LogInformation("Saving application state");
        return _localSettingsService.SaveSettingsAsync(ApplicationStateSettingsKey, _applicationState);
    }

}