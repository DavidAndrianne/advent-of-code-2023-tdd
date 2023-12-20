namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;

public class EnergyGridMirror : IEnergyGridObject
{
    public const char LeftIcon = '/';
    public const char RightIcon = '\\';
    public static char[] chars = [LeftIcon, RightIcon];

    public static Dictionary<CardinalDirection, CardinalDirection> LeftMirrorBounces = new Dictionary<CardinalDirection, CardinalDirection>()
    {
        { CardinalDirection.North, CardinalDirection.East },
        { CardinalDirection.East, CardinalDirection.North },
        { CardinalDirection.South, CardinalDirection.West },
        { CardinalDirection.West, CardinalDirection.South }
    };
    public static Dictionary<CardinalDirection, CardinalDirection> RightMirrorBounces = new Dictionary<CardinalDirection, CardinalDirection>()
    {
        { CardinalDirection.North, CardinalDirection.West },
        { CardinalDirection.East, CardinalDirection.South },
        { CardinalDirection.South, CardinalDirection.East },
        { CardinalDirection.West, CardinalDirection.North }
    };

    public int X { get; set; }
    public int Y { get; set; }
    public char Icon { get; protected set; }
    public Dictionary<CardinalDirection, CardinalDirection> Mapping { get; protected set; }

    public EnergyGridMirror(int x, int y, char c)
    {
        X = x;
        Y = y;
        Mapping = c == LeftIcon 
            ? LeftMirrorBounces 
            : c == RightIcon
                ? RightMirrorBounces
                : throw new ApplicationException($"'{c}' is an invalid mirror symbol");
        Icon = c;
    }

    public EnergyBeam[] Visit(EnergyBeam visitingEnergyBeam)
    {
        visitingEnergyBeam.Direction = Mapping[visitingEnergyBeam.Direction];
        return [visitingEnergyBeam];
    }
}
