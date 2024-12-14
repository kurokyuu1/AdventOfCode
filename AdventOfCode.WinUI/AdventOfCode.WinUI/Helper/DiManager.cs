using Microsoft.Extensions.Hosting;

namespace AdventOfCode.WinUI.Helper;

public static class DiManager
{
    public static IHost Host { get; private set; } = default!;
    public static void Initialize(IHost host)
    {
        Host = host;
    }

    public static TService GetService<TService>() where TService : class
    {
        if (Host.Services.GetService(typeof(TService)) is not TService service)
        {
            throw new ArgumentException(
                $"{typeof(TService)} needs to be registered in the ConfigureServices within the App.xaml.cs");
        }

        return service;
    }
}
