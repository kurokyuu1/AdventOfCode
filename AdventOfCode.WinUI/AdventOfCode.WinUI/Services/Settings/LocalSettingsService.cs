#region "Usings"

using Windows.ApplicationModel;
using Windows.Management.Core;
using Windows.Storage;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Helper;
using AdventOfCode.WinUI.Json;
using AdventOfCode.WinUI.Models;
using Microsoft.Extensions.Options;

#endregion

namespace AdventOfCode.WinUI.Services.Settings;

public sealed class LocalSettingsService : ILocalSettingsService
{
    #region "Constants"

    private const string DefaultApplicationDataFolder = "AdventOfCode";
    private const string DefaultSettingsFileName = "settings.json";

    #endregion

    #region "Variables"

    private readonly string _applicationDataFolder;
    private readonly IAsyncFileService _fileService;

    private readonly string _localApplicationData =
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    private readonly ILogger<LocalSettingsService> _logger;
    private readonly LocalSettingsOptions _options;
    private readonly string _settingsFilePath;

    private bool _isInitialized;

    private Dictionary<string, string> _settings = [];

    private readonly ApplicationDataContainer? _localSettings;

    #endregion

    #region "Constructor"

    public LocalSettingsService(ILogger<LocalSettingsService> logger, IAsyncFileService fileService,
        IOptions<LocalSettingsOptions> options)
    {
        _logger = logger;
        _fileService = fileService;
        _options = options.Value;

        if (RuntimeHelper.IsMsix)
        {
            _localSettings = ApplicationDataManager.CreateForPackageFamily(Package.Current.Id.FamilyName).LocalSettings;
        }

        _applicationDataFolder = Path.Combine(_localApplicationData,
            _options.ApplicationDataFolder ?? DefaultApplicationDataFolder);
        _settingsFilePath = _options.SettingsFileName ?? DefaultSettingsFileName;
    }

    #endregion

    #region "Methods"

    private async Task InitializeAsync()
    {
        if (!_isInitialized && !RuntimeHelper.IsMsix)
        {
            _logger.LogInformation("Initializing local settings for non-packaged app");
            _settings = await _fileService.ReadAsync<Dictionary<string, string>>(_applicationDataFolder, _settingsFilePath) ?? [];
            _isInitialized = true;
        }
    }

    public async Task<T?> ReadSettingsAsync<T>(string key)
    {
        if (RuntimeHelper.IsMsix && _localSettings is { })
        {
            _logger.LogInformation("Reading local settings for packaged app");
            if (_localSettings.Values.TryGetValue(key, out var obj))
            {
                return await JsonHelper.DeserializeAsync<T>((string)obj);
            }
        }
        else
        {
            _logger.LogInformation("Reading local settings for non-packaged app");
            await InitializeAsync();

            if (_settings.TryGetValue(key, out var obj))
            {
                return await JsonHelper.DeserializeAsync<T>(obj);
            }
        }

        return default;
    }

    public async Task SaveSettingsAsync<T>(string key, T value)
    {
        if (RuntimeHelper.IsMsix && _localSettings is { })
        {
            _logger.LogInformation("Saving local settings for packaged app");
            _localSettings.Values[key] = await JsonHelper.SerializeAsync(value);
        }
        else
        {
            _logger.LogInformation("Saving local settings for non-packaged app");
            await InitializeAsync();

            _settings[key] = await JsonHelper.SerializeAsync(value);
            await _fileService.SaveAsync(_applicationDataFolder, _settingsFilePath, _settings);
        }
    }

    #endregion
}
