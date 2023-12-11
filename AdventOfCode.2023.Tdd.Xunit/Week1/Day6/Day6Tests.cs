using AdventOfCode2023.Tdd.Xunit.Util;
using FluentAssertions;
using System.Diagnostics;
using AdventOfCode2023.Tdd.Xunit.Week1.Day6.InputParsers;
using AdventOfCode2023.Tdd.Xunit.Week1.Day6.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day6;

public class Day6Tests
{
    public static string[] SampleInput = [
        "Time:      7  15   30",
        "Distance:  9  40  200"
    ];
    [Fact]
    public void ParseRaces_WithValidInput_ReturnsCorrectRaces()
    {
        // Arrange & Act
        var races = SampleInput.ParseRaces();

        // Assert
        races.Count().Should().Be(3);
        races.Select(x => x.TimeInMilliseconds).Should().BeEquivalentTo(new int[] { 7, 15, 30 });
        races.Select(x => x.RecordDistanceInMillimeter).Should().BeEquivalentTo(new int[] { 9, 40, 200 });
    }

    public static object[][] ExampleDataset = [
        [7, 9, 4, "there are 4 ways to beat the record"],
        [15, 40, 8, "there are 4 ways to beat the record"],
        [30, 200, 9, "there are 9 ways to beat the record"]
        ];
    [Theory]
    [MemberData(nameof(ExampleDataset))]
    public void Race_CalculateTotalWaysToBeatRecord_CorrectSum(int time, int distance, int expectedTotal, string because)
    {
        // Arrange
        var race = new Race(time, distance);

        // Act
        var totalWins = race.CalculateTotalWaysToBeatRecord();

        // Assert
        totalWins.Should().Be(expectedTotal, because);
    }

    [Fact]
    public void Race_CalculateTotalWaysToBeatRecord_Part1()
    {
        // Arrange
        var races = InputReader.ReadLinesForDay(6)
            .ParseRaces();

        // Act
        var total = races.Select(x => x.CalculateTotalWaysToBeatRecord())
            .Aggregate((x,y) => x*y);

        //// Assert
        Debug.WriteLine($"Total multiplied ways to win per race:{total}");

        total.Should().BeGreaterThan(0);
        total.Should().BeLessThan(100000);
    }

    [Fact]
    public void ParseRaces_WithValidInput_ReturnsSingleRace()
    {
        // Arrange & Act
        var race = SampleInput.ParseSingleRace();

        // Assert
        race.TimeInMilliseconds.Should().Be(71530);
        race.RecordDistanceInMillimeter.Should().Be(940200);
    }

    [Fact]
    public void Race_CalculateTotalWaysToBeatRecord_Part2()
    {
        // Arrange
        var race = InputReader.ReadLinesForDay(6)
            .ParseSingleRace();

        // Act
        var total = race.CalculateTotalWaysToBeatRecord2();

        // Assert
        Debug.WriteLine($"Total multiplied ways to win per race:{total}");

        total.Should().BeGreaterThan(35865984);
        total.Should().BeLessThan(100000);
    }
}
