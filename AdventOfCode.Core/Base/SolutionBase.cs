using AdventOfCode.Core.Contracts;
using AdventOfCode.Core.Enumeration;
using AdventOfCode.Core.RegularExpressions;

namespace AdventOfCode.Core.Base;

public abstract class SolutionBase : IAdventModule
{
    #region "Constants"

    private readonly bool _consoleOutput;

    #endregion

    #region "Variables"

    private readonly string _fileName;
    private readonly string _testFilename;

    #endregion

    #region "Constructor"

    protected SolutionBase(string year, string day, bool consoleOutput)
    {
        _fileName = Path.Combine(year, "data", day, "input.txt");
        _testFilename = Path.Combine(year, "data", day, "test.txt");
        _consoleOutput = consoleOutput;
    }

    #endregion

    #region "Methods"

    public abstract Task RunAsync();

    protected void LogToConsole(string message)
    {
        if (_consoleOutput)
        {
            WriteLine(message);
        }
    }

    protected Task<string> InternalReadAllTextAsync(ReadingMode mode = ReadingMode.Input) =>
        ReadAllTextAsync(mode == ReadingMode.Input ? _fileName : _testFilename);

    protected Task<string[]> InternalReadAllLinesAsync(ReadingMode mode = ReadingMode.Input) =>
        ReadAllLinesAsync(mode == ReadingMode.Input ? _fileName : _testFilename);

    protected static void PuzzleOneResult<T>(T msg) => WriteLine($"[Puzzle 1] {msg}");
    protected static void PuzzleTwoResult<T>(T msg) => WriteLine($"[Puzzle 2] {msg}");

    protected static int ConvertToInt(string input) =>
        int.Parse(RegExCollection.ExtractNumbersRegex().Match(input).Value);

    #endregion
}
