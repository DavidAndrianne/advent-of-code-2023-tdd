using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;

public class EnergyGridTile : IHasCarthesianCoordinates
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsEnergized => Visitors.Any();
    public IEnergyGridObject? Object;
    protected List<EnergyBeam> Visitors { get; set; } = new List<EnergyBeam>();
    public List<EnergyGridTile> Neighbors { get; set; } = new List<EnergyGridTile>();

    protected List<CardinalDirection> VisitedDirections { get; set; } = new List<CardinalDirection>();

    public EnergyGridTile(int x, int y, IEnergyGridObject? @object = null)
    {
        X = x; Y = y; 
        Object = @object;
    }

    public EnergyBeam[] Visit(EnergyBeam visitor)
    {
        if (VisitedDirections.Contains(visitor.Direction)) return []; // this path has already been directed light to, break the loop

        VisitedDirections.Add(visitor.Direction);
        Visitors.Add(visitor);
        return Object?.Visit(visitor) ?? [visitor];
    }

    public EnergyGridTile GetPriorVisitedNeighbor(EnergyBeam visitor) 
        => Neighbors.Single(n => n.Visitors.Last() == visitor);
}
