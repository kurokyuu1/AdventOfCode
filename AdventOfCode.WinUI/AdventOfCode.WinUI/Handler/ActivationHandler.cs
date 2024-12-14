using AdventOfCode.WinUI.Contracts.Handler.Activation;

namespace AdventOfCode.WinUI.Handler;

public abstract class ActivationHandler<T> : IActivationHandler where T : class
{
    protected virtual bool CanHandleInternal(T args) => true;
    protected abstract Task HandleInternalAsync(T args);
    public bool CanHandle(object args) => args is T t && CanHandleInternal(t);
    public Task HandleAsync(object args) => HandleInternalAsync((args as T)!);
}
