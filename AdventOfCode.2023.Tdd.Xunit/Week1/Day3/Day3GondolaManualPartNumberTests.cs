using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Extensions;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day3;

public class Day3GondolaManualPartNumberTests
{
    public static string ExampleDataset = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

    [Fact]
    public void ParseGondolaManual_WithValidInput_ParsesSymbolsAndLocations()
    {
        // Arrange
        int[][] expectedPartNumbers = [
            [0, 0, 467],
            [0, 5, 114],
            [2, 2, 35],
            [2, 6, 633],
            [4, 0, 617],
            [5, 7, 58],
            [6, 2, 592],
            [7, 6, 755],
            [9, 1, 664],
            [9, 5, 598]
        ];

        object[][] expectedSymbols = [
            [1, 3, "*"],
            [3, 6, "#"],
            [4, 3, "*"],
            [5, 5, "+"],
            [8, 3, "$"],
            [8, 5, "*"]
        ];

        // Act
        var manual = ExampleDataset.ParseGondolaManual();

        // Assert
        manual.PartNumbers.Should().NotBeEmpty();
        manual.PartNumbers.Count().Should().Be(expectedPartNumbers.Count());
        manual.PartNumbers.Select(x => x.Dump()).Should().BeEquivalentTo(expectedPartNumbers);

        manual.PartNumberSymbols.Should().NotBeEmpty();
        manual.PartNumberSymbols.Count().Should().Be(expectedSymbols.Count());
        manual.PartNumberSymbols.Select(x => x.Dump()).Should().BeEquivalentTo(expectedSymbols);
    }

    [Fact]
    public void PartNumber_IsNextToSymbol_ReturnsExpectedResult()
    {
        // Arrange
        int[] notAdjacent = [114, 58];
        var manual = ExampleDataset.ParseGondolaManual();
        var expected = manual.PartNumbers.Where(x => notAdjacent.All(y => y != x.CodeInt))
            .Select(x => x.Dump())
            .ToArray();

        // Act
        var numbers = manual.PartNumbers.Where(x => x.IsNextToSymbol(manual.PartNumberSymbols))
            .ToArray();

        // Assert
        numbers.Select(x => x.Dump()).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Manual_SumPartNumbersNextToSymbols_ReturnsExpectedSum()
    {
        // Arrange
        var manual = ExampleDataset.ParseGondolaManual();

        // Act
        var sum = manual.SumPartNumbersNextToSymbols();

        // Assert
        sum.Should().Be(4361);
    }

    [Fact]
    public void Manual_SumPartNumbersNextToSymbols_Part1()
    {
        // Arrange
        var manual = InputReader.ReadRawTextForDay(3).ParseGondolaManual();

        // Act
        var sum = manual.SumPartNumbersNextToSymbols();

        // Assert
        Debug.WriteLine($"Valid partnumbers sum:{sum}");

        sum.Should().BeGreaterThan(0);
        sum.Should().BeLessThan(100000);
    }

    [Fact]
    public void Manual_SumGears_Example()
    {
        // Arrange
        var manual = ExampleDataset.ParseGondolaManual();

        // Act
        var sum = manual.SumGearsNextTo2Parts();

        // Assert
        sum.Should().Be(467835);
    }

    [Fact]
    public void Manual_SumGearsNextTo2Numbers_Part2()
    {
        // Arrange
        var manual = InputReader.ReadRawTextForDay(3).ParseGondolaManual();

        // Act
        var sum = manual.SumGearsNextTo2Parts();

        // Assert
        Debug.WriteLine($"Valid gears sum:{sum}");

        sum.Should().BeGreaterThan(0);
    }
}
