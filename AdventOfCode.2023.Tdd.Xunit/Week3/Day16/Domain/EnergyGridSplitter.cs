namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;

public class EnergyGridSplitter : IEnergyGridObject
{
    public const char VerticalIcon = '|';
    public const char HorizontalIcon = '-';

    public int X { get; set; }
    public int Y { get; set; }
    public char Icon { get; protected set; }

    public static CardinalDirection[] HorizontalSplitDirections = [CardinalDirection.North, CardinalDirection.South];
    public static Func<EnergyBeam, EnergyBeam[]> HorizontalSplit = (beam) =>
        [
            new EnergyBeam(beam.X, beam.Y, CardinalDirection.West, parent: beam),
            new EnergyBeam(beam.X, beam.Y, CardinalDirection.East, parent: beam)
        ];

    public static CardinalDirection[] VerticalSplitDirections = [CardinalDirection.East, CardinalDirection.West];
    public static Func<EnergyBeam, EnergyBeam[]> VerticalSplit = (beam) =>
        [
            new EnergyBeam(beam.X, beam.Y, CardinalDirection.North, parent: beam),
            new EnergyBeam(beam.X, beam.Y, CardinalDirection.South, parent: beam)
        ];

    public Func<EnergyBeam, EnergyBeam[]> Split;
    public CardinalDirection[] SplitDirections;

    public EnergyGridSplitter(int x, int y, char c)
    {
        X = x;
        Y = y;
        if(c == HorizontalIcon)
        {
            Split = HorizontalSplit;
            SplitDirections = HorizontalSplitDirections;
        }
        else if(c == VerticalIcon)
        {
            Split = VerticalSplit;
            SplitDirections = VerticalSplitDirections;
        }
        else throw new ApplicationException($"'{c}' is not a valid mirror");
        Icon = c;
    }

    public EnergyBeam[] Visit(EnergyBeam visitingEnergyBeam)
    {
        if (SplitDirections.Contains(visitingEnergyBeam.Direction)) return Split(visitingEnergyBeam);
        return [visitingEnergyBeam];
    }
}
