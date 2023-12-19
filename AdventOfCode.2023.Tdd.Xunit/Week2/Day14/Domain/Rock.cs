using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day14.Domain;

public class Rock : IHasCarthesianCoordinates
{
    public const char RoundRock = 'O';
    public const char SquareRock = '#';
    public bool IsRound { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public Rock(char c, int x, int y)
    {
        IsRound = c == RoundRock;
        if (!IsRound && c != SquareRock) throw new ArgumentException($"'{c}' is not a valid rock!");
        X = x;
        Y = y;
    }

    public void RollNorth() => X -= 1;
    public void RollEast() => Y += 1;
    public void RollSouth() => X += 1;
    public void RollWest() => Y -= 1;

    public override string ToString() 
        => IsRound ? $"{RoundRock}" : $"{SquareRock}";
}
