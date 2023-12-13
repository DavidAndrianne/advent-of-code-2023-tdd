using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week2.Day9.InputParsers;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day9;

public class Day9Tests
{
    public static object[][] ExampleParseInput = [
        ["0 3 6 9 12 15", 2],
        ["1 3 6 10 15 21", 3],
        ["10 13 16 21 30 45", 4]
    ];
    [Theory]
    [MemberData(nameof(ExampleParseInput))]
    public void ParseMeasurements_WithValidInput_ReturnsCorrectValues(string input, int expectedHistoryLines)
    {
        // Arrange & act
        var measurements = input.ParseEcoMeasurement();

        //Assert
        measurements.Values.Count().Should().Be(6);
        measurements.ValueHistories.Count().Should().Be(expectedHistoryLines);
    }

    public static object[][] ExamplePredictInput = [
        ["0 3 6 9 12 15", 18],
        ["1 3 6 10 15 21", 28],
        ["10 13 16 21 30 45", 68]
    ];
    [Theory]
    [MemberData(nameof(ExamplePredictInput))]
    public void EcoMeasurement_PredictNext_ReturnsCorrectValue(string input, int expectedNext)
    {
        // Arrange
        var measurements = input.ParseEcoMeasurement();

        // Act
        var next = measurements.PredictNextValue();

        // Assert
        next.Should().Be(expectedNext);
    }

    [Fact]
    public void EcoMeasurement_PredictNext_Part1()
    {
        // Arrange
        var measurements = InputReader.ReadLinesForDay(9)
            .Select(x => x.ParseEcoMeasurement())
            .ToArray();

        // Act
        var total = measurements.Select(x => x.PredictNextValue())
            .Sum();

        // Assert
        total.Should().BeGreaterThan(1084768249);
    }

    public static object[][] ExamplePredictPreviousInput = [
        ["0 3 6 9 12 15", -3],
        ["1 3 6 10 15 21", 0],
        ["10 13 16 21 30 45", 5]
    ];
    [Theory]
    [MemberData(nameof(ExamplePredictPreviousInput))]
    public void EcoMeasurement_PredictPrevious_ReturnsCorrectValue(string input, int expectedPrevious)
    {
        // Arrange
        var measurements = input.ParseEcoMeasurement();

        // Act
        var previous = measurements.PredictPreviousValue();

        // Assert
        previous.Should().Be(expectedPrevious);
    }

    [Fact]
    public void EcoMeasurement_PredictNext_Part2()
    {
        // Arrange
        var measurements = InputReader.ReadLinesForDay(9)
            .Select(x => x.ParseEcoMeasurement())
            .ToArray();

        // Act
        var total = measurements.Select(x => x.PredictPreviousValue())
            .Sum();

        // Assert
        total.Should().BeGreaterThan(0);
    }
}
