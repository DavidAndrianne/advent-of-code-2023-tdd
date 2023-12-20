using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;

public class EnergyBeam : IHasCarthesianCoordinates
{
    public int X { get; set; }
    public int Y { get; set; }
    public CardinalDirection Direction { get; set; }
    public EnergyBeam Parent { get; protected set; }
    public EnergyBeam(int x, int y, CardinalDirection? direction = null, EnergyBeam parent = null)
    {
        X = x; Y = y;
        Direction = direction ?? CardinalDirection.East;
        Parent = parent;
    }

    public void Move()
    {
        if (Direction == CardinalDirection.East)
        {
            Y++;
            return;
        }
        if (Direction == CardinalDirection.South)
        {
            X++;
            return;
        }
        if (Direction == CardinalDirection.West)
        {
            Y--;
            return;
        }
        // if CardinalDirection.North
        X--;
    }

    public bool IsOutOfBounds(int maxX, int maxY)
    {
        var isOutNorth = X < 0;
        var isOutSouth = X > maxX;
        var isOutWest = Y < 0;
        var isOutEast = Y > maxY;
        return isOutNorth || isOutSouth || isOutWest || isOutEast;
    }
}
