using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week1.Day1.Extensions;
using AdventOfCode2023.Tdd.Xunit.Week1.Day2.Domain;
using AdventOfCode2023.Tdd.Xunit.Week1.Day2.Extensions;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day2;

public class Day2CubeGameValidationTests
{
    [Fact]
    public void ParseGame_WithValidLine_ParsesGame()
    {
        // Arrange
        var line = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";

        // Act
        var game = line.ParseGame();

        // Assert
        game.Id.Should().Be(1);
        game.RedCubeCounts.Should().BeEquivalentTo([4, 1]);
        game.GreenCubeCounts.Should().BeEquivalentTo([2, 2]);
        game.BlueCubeCounts.Should().BeEquivalentTo([3, 6]);
    }

    public static object[][] ExampleDataset =
    [
        ["Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", true, "it's valid"],
        ["Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", true, "it's valid"],
        ["Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", false, "the Elf showed you 20 red cubes at once"],
        ["Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", false, "the Elf showed you 15 blue cubes at once"],
        ["Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", true, "it's valid"]
    ];

    [Theory]
    [MemberData(nameof(ExampleDataset))]
    public async Task ValidateGame_ReturnsExpectedResult(string line, bool expected, string because)
    {
        // arrange
        var game = line.ParseGame();
        var sut = new GameValidator();

        // act
        var result = await sut.ValidateAsync(game);

        // assert
        result.IsValid.Should().Be(expected, because);
    }

    [Fact]
    public void Sum_ForValidGameIds_ReturnsGoodInt()
    {
        // Arrange
        var inputLines = InputReader.ReadLinesForDay(2);

        // Act
        var sum = inputLines.Select(x => x.ParseGame())
            .Where(x => x.IsValid())
            .Select(x => x.Id)
            .Sum();

        // Assert
        Debug.WriteLine($"Valid games sum:{sum}");

        sum.Should().BeGreaterThan(0);
        sum.Should().BeLessThan(10000);
    }

    public static object[][] ExampleDataset2 =
    [
        ["Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48],
        ["Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 12],
        ["Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 1560],
        ["Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 630],
        ["Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 36]
    ];
    [Theory]
    [MemberData(nameof(ExampleDataset2))]
    public void CalculateMinCubePower_ReturnsExpectedInt(string input, int output)
    {
        // Arrange
        var game = input.ParseGame();

        // Act
        var result = game.CalculateMinCubePower();

        // Assert
        result.Should().Be(output);
    }

    [Fact]
    public void Sum_CalculateMinCubePower_ReturnsGoodInt()
    {
        // Arrange
        var inputLines = InputReader.ReadLinesForDay(2);

        // Act
        var sum = inputLines.Select(x => x.ParseGame())
            .Select(x => x.CalculateMinCubePower())
            .Sum();

        // Assert
        Debug.WriteLine($"Valid games sum:{sum}");

        sum.Should().BeGreaterThan(0);
        sum.Should().BeLessThan(100000);
    }
}
