using AdventOfCode.Core.Contracts;

namespace AdventOfCode.Core.Base;

public sealed class AdventResult<T> : IAdventResult
{
    private readonly T? _result;
    public AdventResult(T? result) => _result = result;
    public string AsString() => _result?.ToString() ?? "No Result";

    public override string ToString() => AsString();
}