using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;

public interface IEnergyGridObject : IHasCarthesianCoordinates
{
    public char Icon { get; }
    public EnergyBeam[] Visit(EnergyBeam visitingEnergyBeam);
}
