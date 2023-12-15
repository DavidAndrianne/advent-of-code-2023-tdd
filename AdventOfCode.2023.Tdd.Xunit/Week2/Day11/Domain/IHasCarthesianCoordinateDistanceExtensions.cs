using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day11.Domain;

public static class IHasCarthesianCoordinateDistanceExtensions
{
    public static int ShortestDistanceTo(this IHasCarthesianCoordinates start, IHasCarthesianCoordinates end)
    {
        return Math.Max(start.X, end.X) - Math.Min(start.X, end.X) 
            + Math.Max(start.Y, end.Y) - Math.Min(start.Y, end.Y);
    }
}