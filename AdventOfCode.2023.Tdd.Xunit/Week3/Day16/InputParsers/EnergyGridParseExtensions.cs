using AdventOfCode2023.Tdd.Xunit.Week2.Day14.Domain;
using AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16.InputParsers;

public static class EnergyGridParseExtensions
{
    public static EnergyGrid ToEnergyGrid(this char[][] grid)
    {
        var tiles = new EnergyGridTile[grid.Length][];
        for(var x = 0; x < grid.Length; x++)
        {
            tiles[x] = new EnergyGridTile[grid[x].Length];
            for(var y = 0; y < grid[x].Length; y++)
            {
                var symbol = grid[x][y];
                var gridObject = ObjectFactory.ContainsKey(symbol) 
                    ? ObjectFactory[symbol](x, y) 
                    : null;
                tiles[x][y] = new EnergyGridTile(x, y, gridObject);
            }
        }
        return new EnergyGrid(tiles.SetNeighbors());
    }

    public static EnergyGridTile[][] SetNeighbors(this EnergyGridTile[][] tiles)
    {
        tiles.ToList()
            .ForEach(row => row.ToList()
                .ForEach(tile => tile.Neighbors = tiles.SelectMany(xRow => xRow.Where(neighboringTile => neighboringTile.IsNeighborTo(tile))).ToList()
                )
            );
        return tiles;
    }

    private static bool IsNeighborTo(this EnergyGridTile neighboringTile, EnergyGridTile tile)
    {
        return neighboringTile.X == tile.X && tile.Y.Difference(neighboringTile.Y) == 1
            || (neighboringTile.Y == tile.Y && tile.X.Difference(neighboringTile.X) == 1);
    }

    public static Dictionary<char, Func<int, int, IEnergyGridObject>> ObjectFactory = new Dictionary<char, Func<int, int, IEnergyGridObject>>
    {
        { EnergyGridMirror.LeftIcon, (x, y) => new EnergyGridMirror(x, y, EnergyGridMirror.LeftIcon) },
        { EnergyGridMirror.RightIcon, (x, y) => new EnergyGridMirror(x, y, EnergyGridMirror.RightIcon) },
        { EnergyGridSplitter.HorizontalIcon, (x, y) => new EnergyGridSplitter(x, y, EnergyGridSplitter.HorizontalIcon) },
        { EnergyGridSplitter.VerticalIcon, (x, y) => new EnergyGridSplitter(x, y, EnergyGridSplitter.VerticalIcon) }
    };
}
