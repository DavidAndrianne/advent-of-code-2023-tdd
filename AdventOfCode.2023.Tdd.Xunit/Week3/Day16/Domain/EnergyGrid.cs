using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;

public class EnergyGrid
{
    public EnergyGridTile[][] Tiles { get; set; }
    public int MaxX = 0;
    public int MaxY = 0;

    public EnergyGrid(EnergyGridTile[][] tiles)
    {
        Tiles = tiles;
        MaxX = tiles.Length;
        MaxY = tiles.First().Length;
    }

    public int VisitAndCountEnergized(EnergyBeam startBeam)
    {
        var beams = new List<EnergyBeam> { startBeam };
        var isMoving = false;
        var snapshot = EnergizedString();
        var loops = 0;
        while(beams.Any())
        {
            var beamIterator = beams.ToList();
            foreach (var beam in beamIterator)
            {
                if (isMoving) beam.Move();
                beams.Remove(beam);

                if (beam.IsOutOfBounds(MaxX - 1, MaxY - 1)) continue;

                var newBeams = Tiles[beam.X][beam.Y].Visit(beam);
                beams.AddRange(newBeams);
            }
            isMoving = true; // we don't have to move to the first tile

            // Check if the remaining beams are stuck in a loop, if so terminate
            //if (loops % MaxX == 0)
            //{
            //    var temp = EnergizedString();
            //    if (snapshot == temp) break;
            //    snapshot = temp;
            //}
            loops++;
        }
        return Tiles.Sum(row => row.Where(tile => tile.IsEnergized).Count());
    }

    public string EnergizedString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        for (var x = 0; x < MaxX; x++)
        {
            for (var y = 0; y < MaxY; y++)
            {
                var symbol = Tiles[x][y].IsEnergized
                    ? '#'
                    : '.';
                sb.Append(symbol);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        for (var x = 0; x < MaxX; x++)
        {
            for(var y = 0; y < MaxY; y++)
            {
                sb.Append(Tiles[x][y].Object?.Icon ?? '.');
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}