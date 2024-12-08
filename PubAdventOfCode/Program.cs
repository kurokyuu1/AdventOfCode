using AdventOfCode._2022.Models;
using AdventOfCode._2023.Models;
using AdventOfCode._2024.Models;
using AdventOfCode.Core.Contracts;

var aoc = new Dictionary<int, List<IAdventModule>>
{
    { 2022, Aoc2022Extensions.ModulesFor2022 },
    { 2023, Aoc2023Extensions.ModulesFor2023},
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
