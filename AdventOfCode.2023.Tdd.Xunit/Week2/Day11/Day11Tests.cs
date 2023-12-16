using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week2.Day11.InputParsers;
using FluentAssertions;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day11;

public class Day11Tests
{
    [Fact]
    public void UniverseReadExtensions_DoubleRow_DoublesCorrectly()
    {
        // Arrange
        char[][] input = [
            ['0'],
            ['1'],
            ['2']
        ];
        var inputList = input.Select(x => x.ToList()).ToList();

        // Act
        inputList = inputList.DoubleRow(1);

        // Assert
        input.Count().Should().Be(3); // original unaltered
        inputList.Count().Should().Be(4); // clone altered
        inputList.Sum(x => x.Count(y => y == '1')).Should().Be(2); // 1 doubled
    }

    [Fact]
    public void UniverseReadExtensions_DoubleCol_DoublesCorrectly()
    {
        // Arrange
        char[][] input = [
            ['0', '1', '2']
        ];
        var inputList = input.Select(x => x.ToList()).ToList();

        // Act
        inputList = inputList.DoubleColumn(0);

        // Assert
        input.First().Count().Should().Be(3); // original unaltered
        inputList.First().Count().Should().Be(4); // clone altered
        inputList.Sum(x => x.Count(y => y == '0')).Should().Be(2); // 1 doubled
    }

    public static string ExampleInput = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....";

    [Fact]
    public void UniverseReadExtensions_ParseUniverse_ReturnsUniverse()
    {
        // Arrange
        var input = ExampleInput.Trim().Split(Environment.NewLine)
            .Select(x => x.ToArray())
            .ToArray();

        // Act
        var reading = input.ToUniverseReading();

        // Assert
        reading.Universe.Count().Should().Be(input.Count() + 2, "2 empty lines doubled");
        reading.Universe.First().Count().Should().Be(input.First().Count() + 3, "3 empty columns doubled");
        reading.Universe.Sum(x => x.Count(y => y == '#')).Should().Be(9, "galaxy columns/rows shouldn't double");
    }

    [Fact]
    public void UniverseReading_CountShortestDistances_ReturnsShortestDistancePairs()
    {
        // Arrange
        var input = ExampleInput.Trim().Split(Environment.NewLine)
            .Select(x => x.ToArray())
            .ToArray();
        var reading = input.ToUniverseReading();

        // Act
        var galaxyPairs = reading.GalaxyPairDistances();

        // Assert
        galaxyPairs.Count().Should().Be(36, "There are 36 combinations of galaxies");
        galaxyPairs.First(x => x.Key == 0.006M).Value.Should().Be(15);
        galaxyPairs.First(x => x.Key == 2.005M).Value.Should().Be(17);
        galaxyPairs.First(x => x.Key == 7.008M).Value.Should().Be(5);
        galaxyPairs.Select(x => x.Value).Sum().Should().Be(374, "the shortest distance sum is 374");
    }

    [Fact]
    public void UniverseReading_CountShortestDistances_Part1()
    {
        // Arrange
        var reading = InputReader.ReadCharGrid(11).ToUniverseReading();

        // Act
        var galaxyPairs = reading.GalaxyPairDistances();
        var total = galaxyPairs.Select(x => x.Value).Sum();

        // Assert
        total.Should().BeGreaterThan(1);
    }

    public static object[][] ExpansionMultitudeData = [
        [10, 1030],
        [100, 8410]
    ];
    [Theory]
    [MemberData(nameof(ExpansionMultitudeData))]
    public void UniverseReading_CountShortestDistancesInMultitudes_ReturnsShortestDistancePairs(int multitude, int expected)
    {
        // Arrange
        var input = ExampleInput.Trim().Split(Environment.NewLine)
            .Select(x => x.ToArray())
            .ToArray();
        var reading = input.ToUniverseMultitudeReading(multitude);

        // Act
        var galaxyPairs = reading.GalaxyPairDistances();

        // Assert
        galaxyPairs.Select(x => x.Value).Sum().Should().Be(expected);
    }

    [Fact]
    public void UniverseReading_CountShortestDistanceMultitude_Part2()
    {
        // Arrange
        var oneMillion = 1000000;
        var reading = InputReader.ReadCharGrid(11).ToUniverseMultitudeReading(oneMillion);

        // Act
        var galaxyPairs = reading.GalaxyPairDistances();
        var total = galaxyPairs.Select(x => x.Value).Sum();

        // Assert
        total.Should().BeGreaterThan(1);
    }
}