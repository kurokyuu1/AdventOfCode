namespace AdventOfCode.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AdventModuleAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
