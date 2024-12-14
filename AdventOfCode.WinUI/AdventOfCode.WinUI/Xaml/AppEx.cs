//using System;
//using Kuro.WinUI.Core.Helper;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.UI.Xaml;
//using NLog;
//using NLog.Extensions.Logging;
//using WinUIEx;
//using LogLevel = Microsoft.Extensions.Logging.LogLevel;

//namespace Kuro.WinUI.Core.Xaml;

//public abstract class AppEx<TMainWindow> : Application where TMainWindow : WindowEx, new()
//{
//    protected IConfiguration Configuration { get; set; }
//    public static UIElement? AppTitleBar { get; set; }
//    public static WindowEx MainWindow { get; } = new TMainWindow();
//    protected NLog.ILogger AppLogger;
//    protected AppEx()
//    {
//        Configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
//            .AddJsonFile("appsettings.json", false, true)
//            .AddJsonFile("appsettings.Development.json", optional: true).AddEnvironmentVariables().Build();
//        AppLogger = LogManager
//            .Setup()
//            .LoadConfigurationFromSection(Configuration)
//            .GetCurrentClassLogger();
//        var host = Microsoft.Extensions.Hosting.Host
//            .CreateDefaultBuilder()
//            .UseContentRoot(AppContext.BaseDirectory)
//            .ConfigureLogging((ctx, logging) =>
//            {
//                //logging.ClearProviders();
//                logging.SetMinimumLevel(LogLevel.Trace);
//                logging.AddNLog(Configuration);
//            })
//            .ConfigureServices((ctx, services) =>
//            {
//                RegisterServices(services);
//            })
//            .Build();
//        DiManager.Initialize(host);
//    }

//    protected override void OnLaunched(LaunchActivatedEventArgs args)
//    {
//        base.OnLaunched(args);
//        OnLaunchedEx(args);
//    }

//    protected abstract void RegisterServices(IServiceCollection services);

//    protected abstract void OnLaunchedEx(LaunchActivatedEventArgs args);
//}
