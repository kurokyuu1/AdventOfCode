namespace AdventOfCode.Core.Contracts;

public interface IAdventModule
{
    public Task RunAsync();
    public void WriteResults();
    public IAdventResult? PuzzleResult1 { get; }
    public IAdventResult? PuzzleResult2 { get; }
}
