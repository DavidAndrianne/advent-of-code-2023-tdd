namespace AdventOfCode2023.Tdd.Xunit.Week2.Day11.Domain;

public class UniverseReading
{
    public char[][] Universe { get; set; }
    protected List<Galaxy> Galaxies = new List<Galaxy>();

    public UniverseReading(char[][] universe)
    {
        Universe = universe;
        for(var x = 0; x < universe.Length; x++)
        {
            for(var y = 0; y < universe[x].Length; y++)
            {
                if (universe[x][y] != '.') Galaxies.Add(new Galaxy(x,y));
            }
        }
    }

    public Dictionary<decimal, int> GalaxyPairDistances()
    {
        var result = new Dictionary<decimal, int>();
        var galaxyIndex = 0;
        foreach(var galaxy in Galaxies)
        {
            for(var galaxyIndex2 = galaxyIndex + 1; galaxyIndex2 < Galaxies.Count(); galaxyIndex2++) { 
                var galaxy2 = Galaxies[galaxyIndex2];
                var keyPair = galaxyIndex.PairWith(galaxyIndex2);
                if (result.ContainsKey(keyPair)) break;
                result[keyPair] = galaxy.ShortestDistanceTo(galaxy2);
            }
            galaxyIndex++;
        }
        return result;
    }

    public override string ToString() => Universe.StringifyNestedArray();
}
