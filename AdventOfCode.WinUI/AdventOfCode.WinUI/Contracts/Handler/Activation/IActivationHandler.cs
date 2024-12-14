namespace AdventOfCode.WinUI.Contracts.Handler.Activation;

public interface IActivationHandler
{
    bool CanHandle(object args);

    Task HandleAsync(object args);
}
