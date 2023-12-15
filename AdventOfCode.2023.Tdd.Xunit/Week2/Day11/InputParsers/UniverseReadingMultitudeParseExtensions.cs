using AdventOfCode2023.Tdd.Xunit.Week2.Day11.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day11.InputParsers;

public static class UniverseReadingMultitudeParseExtensions
{
    public const int Empty = 1;
    public const int Galaxy = -1;

    public static UniverseReadingMultitude ToUniverseMultitudeReading(this char[][] input, int multitude)
    {
        var starMap = input.Select(row => row.Select(c => c == '.' ? Empty : Galaxy).ToArray())
            .ToArray()
            .multiplyEmptyRows(multitude)
            .multiplyEmptyCols(multitude);
        return new UniverseReadingMultitude(starMap);
    }

    private static int[][] multiplyEmptyCols(this int[][] map, int multitude)
    {
        for (var y = 0; y < map.First().Count(); y++)
        {
            var isEmpty = true;
            for (var x = 0; x < map.Count(); x++)
            {
                if (map[x][y] == Galaxy)
                {
                    isEmpty = false;
                    break;
                }
            }
            if (!isEmpty) continue;

            for (var x = 0; x < map.Count(); x++) map[x][y] = multitude;
        }
        return map;
    }

    private static int[][] multiplyEmptyRows(this int[][] map, int multitude)
    {
        for (var x = 0; x < map.Count(); x++)
        {
            var isEmptyRow = map[x].All(x => x == Empty);
            if (!isEmptyRow) continue;

            map[x] = map.Select(x => multitude).ToArray();
        }

        return map;
    }
}
