using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week1.Day4.Domain;
using AdventOfCode2023.Tdd.Xunit.Week1.Day4.Extensions;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day4;

public class Day4Tests
{
    [Fact]
    public void ParseScratchCard_WithValidInput_ReturnsCorrectScratchCard()
    {
        // Arrange
        var line = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53";
        var expectedId = 1;
        int[] expectedWinningNumbers = [41, 48, 83, 86, 17];
        int[] expectedEntries = [83, 86, 6, 31, 17, 9, 48, 53];

        // Act
        var card = line.ParseScratchCard();

        // Assert
        card.Id.Should().Be(expectedId);
        card.WinningNumbers.Should().BeEquivalentTo(expectedWinningNumbers);
        card.Entries.Should().BeEquivalentTo( expectedEntries);
    }

    public static object[][] ExampleDataset = [
        ["Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 8, "1 for 1st match, then doubled 3 times for each of the 3 matches after"],
        ["Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2, "2 winning numbers"],
        ["Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1", 2, "2 winning numbers"],
        ["Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83", 1, "1 winning number"],
        ["Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36", 0, "no winning numbers"],
        ["Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", 0, "no winning numbers"]
        ];

    [Theory]
    [MemberData(nameof(ExampleDataset))]
    public void ScratchCard_WithValidInput_CorrectSum(string line, int expectedScore, string because)
    {
        // Arrange
        var card = line.ParseScratchCard();

        // Act
        var score = card.Score;

        // Assert
        score.Should().Be(expectedScore, because);
    }

    [Fact]
    public void ScratchCard_Score_Part1()
    {
        // Arrange
        var cards = InputReader.ReadLinesForDay(4)
            .Select(x => x.ParseScratchCard())
            .ToArray();

        // Act
        var sum = cards.Select(x => x.Score).Sum();

        // Assert
        Debug.WriteLine($"Total scrathcards score sum:{sum}");

        sum.Should().BeGreaterThan(0);
        sum.Should().BeLessThan(100000);
    }

    [Fact]
    public void ScratchCardWinner_WithValidInput_CorrectCounts()
    {
        // Arrange
        object[][] inputExpectedCountBecause = [
            ["Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 1, "1 + no winning numbers"],
            ["Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2, "1 + won by card 1(+1)"],
            ["Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1", 4, "1 + won by cards 1(+1) & 2(+2)"],
            ["Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83", 8, "1 + won by cards 1(+1), 2(+2) & 3(+4)"],
            ["Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36", 14, "1 + won by cards 1(+1), 3(+4) & 4(+8)"],
            ["Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", 1, "1 + no winning numbers"]
        ];
        var lines = inputExpectedCountBecause.Select(x => x[0].ToString());
        var cards = lines.Select(x => x.ParseScratchCard());
        var expectedCounts = inputExpectedCountBecause.Select((x, i) => new { Index = i, Count = x[1] })
            .ToDictionary(x => x.Index, x => x.Count);

        // Act
        var winner = new ScratchCardWinner(cards);

        // Assert
        winner.ScratchCardCounts.Should().BeEquivalentTo(expectedCounts);
        winner.TotalScratchCards.Should().Be(30);
    }

    [Fact]
    public void ScratchCardWinner_WithValidInput_Part2()
    {
        // Arrange
        var cards = InputReader.ReadLinesForDay(4)
            .Select(x => x.ParseScratchCard());
        var winner = new ScratchCardWinner(cards);

        // Act
        var sum = winner.TotalScratchCards;

        // Assert
        Debug.WriteLine($"Total won scratchcards:{sum}");

        sum.Should().BeGreaterThan(0);
    }
}
