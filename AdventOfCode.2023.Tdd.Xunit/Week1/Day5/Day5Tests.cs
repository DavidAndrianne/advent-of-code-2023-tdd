using AdventOfCode2023.Tdd.Xunit.Util;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5;

public class Day6Tests
{
    [Fact]
    public void ParseTodo_WithValidInput_ReturnsCorrectScratchCard()
    {
        // Arrange
        var line = "todo";

        // Act
        //var todo = line.ParseTodo();

        // Assert
        //card.Id.Should().Be(expectedId);
        //card.WinningNumbers.Should().BeEquivalentTo(expectedWinningNumbers);
        //card.Entries.Should().BeEquivalentTo( expectedEntries);
    }

    public static object[][] ExampleDataset = [
        // todo
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
