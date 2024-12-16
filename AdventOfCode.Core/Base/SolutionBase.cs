using System.Diagnostics.CodeAnalysis;
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
        _fileName = Path.Combine(AppContext.BaseDirectory, year, "data", day, "input.txt");
        _testFilename = Path.Combine(AppContext.BaseDirectory, year, "data", day, "test.txt");
        _consoleOutput = consoleOutput;
    }

    #endregion

    #region "Methods"

    public abstract Task RunAsync();
    public IAdventResult? PuzzleResult1 { get; private set; }
    public IAdventResult? PuzzleResult2 { get; private set; }

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

    protected ReadOnlySpan<char> InternalReadAllText(ReadingMode mode = ReadingMode.Input) =>
        ReadAllText(mode == ReadingMode.Input ? _fileName : _testFilename);

    protected ReadOnlySpan<string> InternalReadAllLines(ReadingMode mode = ReadingMode.Input) =>
        ReadAllLines(mode == ReadingMode.Input ? _fileName : _testFilename);

    protected void PuzzleOne(string message) => WriteLine($"[Puzzle 1] {message}");
    protected void PuzzleTwo(string message) => WriteLine($"[Puzzle 2] {message}");

    protected void SetPuzzleOneResult<TResult>(TResult result)
    {
        PuzzleResult1 = new AdventResult<TResult>(result);
    }

    protected void SetPuzzleTwoResult<TResult>(TResult result)
    {
        PuzzleResult2 = new AdventResult<TResult>(result);
    }

    protected static int ConvertToInt(string input) =>
        int.Parse(RegExCollection.ExtractNumbersRegex().Match(input).Value);

    #endregion
}
