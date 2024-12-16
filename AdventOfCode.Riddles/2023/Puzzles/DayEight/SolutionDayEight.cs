#region "Usings"

using AdventOfCode.Core.Helper;
using AdventOfCode.Core.RegularExpressions;

#endregion

namespace AdventOfCode.Riddles._2023.Puzzles.DayEight;

[AdventModule("Day Eight")]
internal sealed class SolutionDayEight : SolutionBase
{
    #region "Constructor"

    public SolutionDayEight() : base("2023", "08", true)
    {
    }

    #endregion

    #region "Methods"

    private static InstructionNode ParseInstruction(string instruction)
    {
        var split = RegExCollection.ExtractAZNodes().Matches(instruction).Select(x => x.Value).ToArray();
        return new(split[0], split[1], split[2]);
    }

    public override async Task RunAsync()
    {
        var data = (await InternalReadAllTextAsync()).SplitByDoubleNewLine();
        var direction = data[0].Select(x => x == 'L' ? InstructionReadDirection.Left : InstructionReadDirection.Right)
            .ToArray();
        var instructions = data[1].SplitByNewLine().Select(ParseInstruction).ToList();
        var puzzleTwoResult = instructions
            .Where(x => x.Instruction.EndsWith('A'))
            .Select(x => StepsFromAToZ(x.Instruction, "Z", instructions, direction))
            .Aggregate(1UL, MathHelper.LowestCommonMultiple);

        SetPuzzleOneResult(StepsFromAToZ("AAA", "ZZZ", instructions, direction));
        SetPuzzleTwoResult(puzzleTwoResult);
    }

    private static ulong StepsFromAToZ(string startingNode, string marker, IReadOnlyList<InstructionNode> nodes,
        IReadOnlyList<InstructionReadDirection> directions)
    {
        var steps = 0UL;
        var instructionSteps = 0;
        var currentNode = startingNode;

        while (!currentNode.EndsWith(marker))
        {
            if (currentNode == marker)
            {
                return steps;
            }

            var item = nodes.First(x => x.Instruction == currentNode);
            currentNode = directions[instructionSteps % directions.Count] switch
            {
                InstructionReadDirection.Left => item.Left,
                InstructionReadDirection.Right => item.Right,
            };
            steps++;
            instructionSteps++;
        }

        return steps;
    }

    #endregion

    #region "Nested"

    private enum InstructionReadDirection
    {
        None,
        Left,
        Right,
    }

    #endregion
}

internal sealed record InstructionNode(string Instruction, string? Left, string? Right);
