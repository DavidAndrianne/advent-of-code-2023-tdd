using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day14.Domain;

public static class CarhestianCardinalDirectionExtensions
{
    public static bool IsNorthTo(this IHasCarthesianCoordinates a, IHasCarthesianCoordinates b) 
        => a.Y == b.Y && a.X.Difference(b.X) == 1 && a.X < b.X;

    public static bool IsSouthTo(this IHasCarthesianCoordinates a, IHasCarthesianCoordinates b)
        => a.Y == b.Y && a.X.Difference(b.X) == 1 && a.X > b.X;

    public static bool IsEastTo(this IHasCarthesianCoordinates a, IHasCarthesianCoordinates b)
        => a.X == b.X && a.Y.Difference(b.Y) == 1 && a.Y > b.Y;

    public static bool IsWestTo(this IHasCarthesianCoordinates a, IHasCarthesianCoordinates b)
        => a.X == b.X && a.Y.Difference(b.Y) == 1 && a.Y < b.Y;

    public static int Difference(this int a, int b) 
        => Math.Max(a, b) - Math.Min(a, b);
}
