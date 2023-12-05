using AdventOfCode2023.Tdd.Xunit.Util;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5;

public class Day6Tests
{
    public readonly string ExampleInput = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    [Fact]
    public void ParseAlmanac_WithValidInput_ReturnsCorrectValues()
    {
        // Act & Act
        var almanac = ExampleInput.ParseAlmanac();

        // Assert
        //card.Id.Should().Be(expectedId);
        //card.WinningNumbers.Should().BeEquivalentTo(expectedWinningNumbers);
        //card.Entries.Should().BeEquivalentTo( expectedEntries);
    }

    public static object[][] ExampleDataset = [
        // todo
        []
        ];
    [Theory]
    [MemberData(nameof(ExampleDataset))]
    public void ScratchCard_WithValidInput_CorrectSum(string line, object expected, string because)
    {
        // Arrange
        var todo = "";//line.ParseTodo();

        // Act
        //var score = todo.Score;

        // Assert
        //score.Should().Be(expectedScore, because);
    }

    [Fact]
    public void Todo_Todo_Part1()
    {
        // Arrange
        var todos = InputReader.ReadLinesForDay(5)
            .Select(x => x/*.ParseTodo()*/)
            .ToArray();

        // Act
        //var sum = todos.Select(x => x.Score).Sum();

        //// Assert
        //Debug.WriteLine($"Total scrathtodos score sum:{sum}");

        //sum.Should().BeGreaterThan(0);
        //sum.Should().BeLessThan(100000);
    }
}
