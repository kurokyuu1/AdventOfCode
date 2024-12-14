using AdventOfCode.Core.Contracts;
using AdventOfCode.Core.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var logger = LogManager.Setup().LoadConfigurationFromSection(configuration).GetCurrentClassLogger();

var host = Host.CreateDefaultBuilder()
    .UseContentRoot(AppContext.BaseDirectory)
    .ConfigureLogging((ctx, logging) =>
    {
        logging.SetMinimumLevel(LogLevel.Trace);
        logging.AddNLog(configuration);
    })
    .ConfigureServices((context, services) =>
    {
        
    })
    .Build();

DependencyInjectionManager.Initialize(host);

var aoc = new Dictionary<int, List<IAdventModule>>
{
    { 2022, Aoc2022Extensions.ModulesFor2022 },
    { 2023, Aoc2023Extensions.ModulesFor2023 },
    { 2024, Aoc2024Extensions.ModulesFor2024 },
};

WriteLine("Enter the year to run the Advent of Code modules for:");
var year = ReadLine()?.ToInt() ?? DateTime.Now.Year;

if (!aoc.TryGetValue(year, out var modules))
{
    WriteLine($"No modules found for year {year}");
    Read();
    return;
}

foreach (var item in modules)
{
    WriteLine($"Running module [{(Attribute.GetCustomAttribute(item.GetType(), typeof(AdventModuleAttribute)) as AdventModuleAttribute)?.Name}]");

    await item.RunAsync();
}

Read();
