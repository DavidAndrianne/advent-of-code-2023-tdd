using AdventOfCode2023.Tdd.Xunit.Week2.Day11.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day11.InputParsers;

public static class UniverseReadingParseExtensions
{
    public static UniverseReading ToUniverseReading(this char[][] input)
    {
        var reading = doubleEmptyRowsAndCols(input);
        return new UniverseReading(reading.Select(x => x.ToArray()).ToArray());
    }

    private static List<List<char>> doubleEmptyRowsAndCols(char[][] input)
    {
        var reading = input.Select(x => x.ToList())
            .ToList()
            .doubleRows(input)
            .doubleCols(input);
        return reading;
    }

    private static List<List<char>> doubleCols(this List<List<char>> reading, char[][] input)
    {
        var colsDoubled = 0;
        for (var y = 0; y < input.First().Count(); y++)
        {
            var isEmpty = true;
            for (var x = 0; x < input.Count(); x++)
            {
                if (input[x][y] != '.')
                {
                    isEmpty = false;
                    break;
                }
            }
            if (isEmpty)
            {
                reading = reading.DoubleColumn(y + colsDoubled);
                colsDoubled++;
            }
        }

        return reading;
    }

    private static List<List<char>> doubleRows(this List<List<char>> reading,char[][] input)
    {
        var rowsDoubled = 0;
        for (var x = 0; x < input.Count(); x++)
        {
            if (input[x].All(x => x == '.'))
            {
                reading = reading.DoubleRow(x + rowsDoubled);
                rowsDoubled++;
            }
        }

        return reading;
    }

    public static List<List<T>> DoubleRow<T>(this List<List<T>> input, int row) 
        => input.Take(row) // part1
            .Concat(new List<List<T>> { input[row] }) // double row
            .Concat(input.Skip(row).Take(input.Count() - row)) // part2
            .ToList();

    public static List<List<T>> DoubleColumn<T>(this List<List<T>> input, int col)
    {
        for (var x = 0; x < input.Count(); x++)
        {
            input[x] = input[x].Take(col) // part1
                .Concat(new List<T> { input[x][col] }) // double col
                .Concat(input[x].Skip(col).Take(input[x].Count() - col)) // part2
                .ToList();
        }
        return input;
    }
}