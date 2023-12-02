using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week1.Day1.Extensions;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day1;

public class Day1TrebuchetCalibrationTests
{
    public static object[][] ExampleDataset =
    [
        ["1abc2", 12],
        ["pqr3stu8vwx", 38],
        ["a1b2c3d4e5f", 15],
        ["treb7uchet", 77]
    ];

    [Theory]
    [MemberData(nameof(ExampleDataset))]
    public void ParseToFirstAndLastInt_WithoutDigitReading_ReturnsExpectedInt(string input, int output)
    {
        // Arrange & Act
        var result = input.ParseToFirstAndLastInt();

        // Assert
        result.Should().Be(output);
    }

    [Fact]
    public void Sum_ForParsedInput_ReturnsGoodInt()
    {
        // Arrange
        var inputLines = InputReader.ReadLinesForDay(1);

        // Act
        var result = inputLines.Select(x => x.ParseToFirstAndLastInt())
            .Sum();

        // Assert
        Debug.WriteLine($"Trebuchet callibration input:{result}");

        result.Should().BeGreaterThan(0);
        result.Should().BeLessThan(25756715);
    }

    public static object[][] ExampleDataset2 =
    [
        ["two1nine", 29],
        ["eightwothree", 83],
        ["abcone2threexyz", 13],
        ["xtwone3four", 24],
        ["4nineeightseven2", 42],
        ["zoneight234", 14],
        ["7pqrstsixteen", 76]
    ];

    [Theory]
    [MemberData(nameof(ExampleDataset2))]
    public void ParseToFirstAndLastInt_WithDigitReading_ReturnsExpectedInt(string input, int output)
    {
        // Arrange & Act
        var result = input.ParseToFirstAndLastInt(true);

        // Assert
        result.Should().Be(output);
    }

    [Fact]
    public void Sum_ForParsedInputWithDigitReading_ReturnsGoodInt()
    {
        // Arrange
        var inputLines = InputReader.ReadLinesForDay(1);

        // Act
        var results = inputLines.Select(x => new { input = x, result = x.ParseToFirstAndLastInt(true) });
        var sum = results.Sum(x => x.result);

        // Assert
        Debug.WriteLine($"Trebuchet callibration input:{sum}");

        sum.Should().BeGreaterThan(0);
        sum.Should().BeLessThan(55440);
    }
}
