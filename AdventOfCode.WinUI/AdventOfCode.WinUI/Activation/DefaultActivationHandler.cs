using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Handler;
using AdventOfCode.WinUI.ViewModels;

namespace AdventOfCode.WinUI.Activation;

public sealed class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly ILogger<DefaultActivationHandler> _logger;
    private readonly INavigationService _service;

    public DefaultActivationHandler(ILogger<DefaultActivationHandler> logger, INavigationService service)
    {
        _logger = logger;
        _service = service;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
        => _service.Frame?.Content is null;

    protected override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _service.NavigateTo(typeof(MainWindowViewModel).FullName!, args.Arguments);

        return Task.CompletedTask;
    }
}