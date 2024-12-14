using System.Numerics;
using Microsoft.Extensions.Hosting;

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

public static class DependencyInjectionManager
{
    public static IHost Host { get; private set; } = default!;

    public static void Initialize(IHost host) => Host = host;

    public static TService GetService<TService>() where TService : class
    {
        if (Host.Services.GetService(typeof(TService)) is not TService service)
        {
            throw new ArgumentException($"Service of type {typeof(TService).Name} not found in the service collection, please register it correctly in the Program.cs file.");
        }

        return service;
    }
}