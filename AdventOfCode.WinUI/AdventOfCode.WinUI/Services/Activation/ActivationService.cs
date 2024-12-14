using AdventOfCode.WinUI.Contracts.Handler.Activation;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Handler;
using AdventOfCode.WinUI.Helper;
using AdventOfCode.WinUI.Pages;
using Microsoft.UI.Xaml.Controls;

namespace AdventOfCode.WinUI.Services.Activation;

public sealed class ActivationService : IActivationService
{
    private readonly ILogger<ActivationService> _logger;
    private readonly ActivationHandler<LaunchActivatedEventArgs> _defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activationHandlers;
    private readonly IThemeSelectorService _themeSelectorService;
    private UIElement? _shell = null;

    public ActivationService(ILogger<ActivationService> logger, ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService)
    {
        _logger = logger;
        _defaultHandler = defaultHandler;
        _activationHandlers = activationHandlers;
        _themeSelectorService = themeSelectorService;
    }

    public async Task ActivateAsync(object activationArgs)
    {
        // Execute tasks before activation.
        await InitializeAsync();
        _logger.LogInformation("Invoked Activate Async with args: {ActivationArgs}", activationArgs);

        // Set the MainWindow Content.
        if (App.MainWindow.Content == null)
        {
            _logger.LogInformation("Content of MainWindow is empty so we'll set the shell page as its content.");
            _shell = DiManager.GetService<ShellPage>();
            App.MainWindow.Content = _shell ?? new Frame();
        }

        // Handle activation via ActivationHandlers.
        await HandleActivationAsync(activationArgs);

        _logger.LogInformation("Activation handled successfully.");
        // Activate the MainWindow.
        App.MainWindow.Activate();

        _logger.LogInformation("MainWindow activated successfully.");

        // Execute tasks after activation.
        await StartupAsync();
        _logger.LogInformation("Startup tasks executed successfully.");
    }

    private async Task HandleActivationAsync(object activationArgs)
    {
        var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));


        if (activationHandler != null)
        {
            _logger.LogInformation("ActivationHandler: {ActivationHandler} seems to be able to handle the activation.", activationHandler.GetType().Name);
            await activationHandler.HandleAsync(activationArgs);
        }

        if (_defaultHandler.CanHandle(activationArgs))
        {
            _logger.LogInformation("DefaultHandler: {DefaultHandler} seems to be able to handle the activation.", _defaultHandler.GetType().Name);
            await _defaultHandler.HandleAsync(activationArgs);
        }
    }

    private Task InitializeAsync() => _themeSelectorService.InitializeAsync();

    private Task StartupAsync() => _themeSelectorService.SetRequestedThemeAsync();
}
