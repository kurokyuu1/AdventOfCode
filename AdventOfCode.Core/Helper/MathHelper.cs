using System.Numerics;

namespace AdventOfCode.Core.Helper;

public static class MathHelper
{
    public static T LowestCommonMultiple<T>(T a, T b) where T : struct, IComparable, IEquatable<T>, INumberBase<T>, IModulusOperators<T, T, T> 
        => (a * b) / GreatestCommonDivisor(a, b);

    public static T GreatestCommonDivisor<T>(T a, T b) where T : struct, IComparable, IEquatable<T>, INumberBase<T>, IModulusOperators<T, T, T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}
