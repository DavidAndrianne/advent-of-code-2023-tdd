using System.Text;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day11.Domain;

public class UniverseReadingMultitude
{
    public int[][] Universe { get; set; }
    protected List<Galaxy> Galaxies = new List<Galaxy>();

    public UniverseReadingMultitude(int[][] universe)
    {
        Universe = universe;
        for (var x = 0; x < universe.Length; x++)
        {
            for (var y = 0; y < universe[x].Length; y++)
            {
                if (universe[x][y] == -1) Galaxies.Add(new Galaxy(x, y));
            }
        }
    }

    public Dictionary<decimal, long> GalaxyPairDistances()
    {
        var result = new Dictionary<decimal, long>();
        var galaxyIndex = 0;
        foreach (var galaxy in Galaxies)
        {
            for (var galaxyIndex2 = galaxyIndex + 1; galaxyIndex2 < Galaxies.Count(); galaxyIndex2++)
            {
                var galaxy2 = Galaxies[galaxyIndex2];
                var keyPair = galaxyIndex.PairWith(galaxyIndex2);
                if (result.ContainsKey(keyPair)) break;
                result[keyPair] = ShortestDistanceTo(galaxy, galaxy2);
            }
            galaxyIndex++;
        }
        return result;
    }

    protected long ShortestDistanceTo(Galaxy g1, Galaxy g2)
    {
        var trueSourceX = GetTrueLocationX(g1.X, g1.Y);
        var trueDestinationX = GetTrueLocationX(g2.X, g2.Y);
        var trueSourceY = GetTrueLocationY(g1.X, g1.Y);
        var trueDestinationY = GetTrueLocationY(g2.X, g2.Y);

        return Math.Max(trueSourceX, trueDestinationX) - Math.Min(trueSourceX, trueDestinationX)
            + Math.Max(trueSourceY, trueDestinationY) - Math.Min(trueSourceY, trueDestinationY);
    }

    protected long GetTrueLocationX(int x, int y)
    {
        var trueLocationX = 0;
        for (var i = 0; i < x; i++)
        {
            var val = Universe[i][y];
            if (val < 0) val *= -1; // invert galaxy
            trueLocationX += val;
        }
        return trueLocationX;
    }

    protected long GetTrueLocationY(int x, int y)
    {
        var trueLocationY = 0;
        for (var j = 0; j < y; j++)
        {
            var val = Universe[x][j];
            if (val < 0) val *= -1; // invert galaxy
            trueLocationY += val;
        }
        return trueLocationY;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < Universe.Length; i++)
        {
            for (var j = 0; j < Universe.First().Length; j++)
                sb.Append($"|{Universe[i][j]:D3}|");
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
