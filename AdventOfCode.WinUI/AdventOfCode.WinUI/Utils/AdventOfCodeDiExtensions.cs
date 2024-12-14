using AdventOfCode.WinUI.Activation;
using AdventOfCode.WinUI.Contracts;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Handler;
using AdventOfCode.WinUI.Models;
using AdventOfCode.WinUI.Pages;
using AdventOfCode.WinUI.Services;
using AdventOfCode.WinUI.Services.Activation;
using AdventOfCode.WinUI.Services.Appearance;
using AdventOfCode.WinUI.Services.Navigation;
using AdventOfCode.WinUI.Services.Settings;
using AdventOfCode.WinUI.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.WinUI.Utils;

public static class AdventOfCodeDiExtensions
{
    public static IServiceCollection AddBaseServices(this IServiceCollection services)
        => services
            .AddViewModels()
            .AddPages()
            .AddServices();

    public static IServiceCollection AddViewModels(this IServiceCollection services)
        => services
            .AddTransient<AboutViewModel>()
            .AddTransient<MainWindowViewModel>()
            .AddTransient<SettingsViewModel>()
            .AddTransient<ShellViewModel>();

    public static IServiceCollection AddPages(this IServiceCollection services) =>
        services
            .AddTransient<AboutPage>()
            .AddTransient<MainWindow>()
            .AddTransient<ShellPage>()
            .AddTransient<SettingsPage>()
            .AddTransient<MainPage>();

    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
                .AddSingleton<IPageService, PageService>()
                .AddSingleton<ILocalSettingsService, LocalSettingsService>()
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IAsyncFileService, FileService>()
                .AddSingleton<IThemeSelectorService, ThemeSelectorService>()
                .AddSingleton<IApplicationStateService, ApplicationStateService>()
                .AddSingleton<IActivationService, ActivationService>()
                .AddTransient<INavigationViewService, NavigationViewService>()
                .AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

    public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<LocalSettingsOptions>(configuration.GetSection(nameof(LocalSettingsOptions)));
}
