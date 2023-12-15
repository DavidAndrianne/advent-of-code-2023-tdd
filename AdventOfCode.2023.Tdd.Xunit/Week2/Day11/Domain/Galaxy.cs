using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day11.Domain;

public class Galaxy : IHasCarthesianCoordinates
{
    public int X { get; set; }

    public int Y { get; set; }

    public Galaxy(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
        => $"#({X},{Y})";
}
