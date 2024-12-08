This is the first real public repo I made for [Advent of Code](https://adventofcode.com).
There are the years of 2022 - 2024, had to clean up 2022 - 2023 a bit, and did some small changes, but not much from the initial code.
I did not every day, so there are missing days, and some solutions have not fully solved the puzzle some have only part1, or code is in general broken. 

I'm doing the challanges with C# and .NET 9.

If you want to run the code by yourself you have to provide your own data inputs from the website.

The project structure is like this:

```txt
- {year}
  -- data
	--- {day with leading 0}
	  --- test.txt
	  --- input.txt
- Models (if specific to the day if used often you can change it
  -- Day{DayNumber in words} // example DayOne, DayTwo...
- Puzzles
  -- Day{DayNumber in words} // example DayOne, DayTwo
    - File/Class Name: Solution{DayNumber in Words}.cs
    - Classes are constructed like this:
```
```cs
  [AdventModule("Day One")]
  public sealed class SolutionDayOne : SolutionBase
  {
      public SolutionDayOne() : base("2024","01", true) {}
  
      public override Task RunAsync() => null;
  }
  ```
In the `Program.cs` then you have to write this:
```cs
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
```

And this should be it.
