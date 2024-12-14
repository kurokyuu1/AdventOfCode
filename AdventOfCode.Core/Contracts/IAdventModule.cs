namespace AdventOfCode.Core.Contracts;

public interface IAdventModule
{
    public Task<(object Result1, object Result2)> RunAsync();
}
