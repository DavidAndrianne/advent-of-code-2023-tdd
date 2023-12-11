using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week1.Day7.Domain;
using AdventOfCode2023.Tdd.Xunit.Week1.Day7.InputParsers;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day7;

public class Day7Tests
{
    public static object[][] ExampleInput = [
        ["32T3K 765", "32T3K", 765, HandType.OnePair],
        ["T55J5 684", "T55J5", 684, HandType.ThreeOfAKind],
        ["KK677 28 ", "KK677", 28, HandType.TwoPair],
        ["KTJJT 220", "KTJJT", 220, HandType.TwoPair],
        ["QQQJA 483", "QQQJA", 483, HandType.ThreeOfAKind]
    ];
    [Theory]
    [MemberData(nameof(ExampleInput))]
    public void ParseHand_WithValidInput_ReturnsCorrectValues(string line, string expectedCards, double expectedBid, HandType expectedType)
    {
        // Arrange & Act
        var hand = line.ParseHand();

        // Assert
        hand.Cards.Should().Be(expectedCards);
        hand.Bid.Should().Be(expectedBid);
        hand.Type.Name.Should().Be(expectedType.Name);
    }

    public static object[][] HandCompareToDataset = [
        [new Hand("32T3K", 1), new Hand("32T3K", 2), 0, "they're equal"],
        [new Hand("32T3K", 1), new Hand("T55J5", 2), -2, "three of a kind is greater than a pair"],
        [new Hand("KK677", 1), new Hand("KTJJT", 2), 3, "same type, same 1st card, kind is 3 higher than 10"],
        ];
    [Theory]
    [MemberData(nameof(HandCompareToDataset))]
    public void Hand_CompareTo_CorrectResult(Hand hand1, Hand hand2, int expectedResult, string because)
    {
        // Arrange & Act
        var result = hand1.CompareTo(hand2);

        // Assert
        result.Should().Be(expectedResult, because);
    }

    [Fact]
    public void Hand_Sort_SortsByTypeAndOrder()
    {
        // Arrange
        var hand1 = new Hand("32T3K", 1);
        var hand2 = new Hand("32T3K", 2);
        var hand3 = new Hand("T55J5", 5);
        var hand4 = new Hand("KTJJT", 3);
        var hand5 = new Hand("KK677", 4);
        Hand[] hands = [hand3, hand5, hand4, hand1, hand2];
        var expectedCards = hands.OrderBy(x => x.Bid).Select(x => x.Cards).ToArray();

        // Act
        var sorted = hands.OrderBy(x => x).ToArray();

        // Assert
        for(var i = 0; i < expectedCards.Length; i++)
        {
            sorted[i].Cards.Should().Be(expectedCards[i]);
        }
    }

    [Fact]
    public void Hands_SortedMulitipliedBid_Example()
    {
        // Arrange
        var hands = ExampleInput.Select(x => x[0].ToString())
            .Select(x => x.ParseHand())
            .ToArray();

        // Act
        var sum = hands.OrderBy(x => x)
                       .Select((x, i) => x.Bid * (i + 1))
                       .Sum();

        // Assert
        Debug.WriteLine($"Total bids multiplied by ranks:{sum}");

        sum.Should().Be(6440);
    }

    [Fact]
    public void Hands_SortedMulitipliedBid_Part1()
    {
        // Arrange
        var hands = InputReader.ReadLinesForDay(7)
            .Select(x => x.ParseHand())
            .ToArray();

        // Act
        var sum = hands.OrderBy(x => x)
                       .Select((x, i) => x.Bid*(i+1))
                       .Sum();

        // Assert
        Debug.WriteLine($"Total bids multiplied by ranks:{sum}");

        sum.Should().BeGreaterThan(0);
        sum.Should().BeLessThan(100000);
    }

    public static object[][] HandTypeJokerParseData = [
        ["JJQJQ", HandType.FiveOfAKind, "because jokers make 5 of a kind"],
        ["JJ8QQ", HandType.FourOfAKind, "because jokers make 4 of a kind"],
        ["222QQ", HandType.FullHouse, "because jokers make a full house"],
        ["2J8JQ", HandType.ThreeOfAKind, "because jokers make 3 of a kind"],
        ["8A8AQ", HandType.TwoPair, "because jokers make 2 pair"],
        ["A234J", HandType.OnePair, "because jokers make 1 pair"],
    ];

    [Theory]
    [MemberData(nameof(HandTypeJokerParseData))]
    public void HandType_ParseWithJoker_RecognizesType(string cards, HandType expectedType, string because)
    {
        // Arrange & Act
        var type = HandType.ParseJoker(cards);

        // Assert
        type.Name.Should().Be(expectedType.Name, because);
    }

    [Fact]
    public void Hand_SortWithJoker_SortsByTypeAndOrder()
    {
        // Arrange
        var hand1 = new Hand("32T3K", 1, true);
        var hand2 = new Hand("T55J5", 2, true);
        var hand3 = new Hand("KK677", 5, true);
        var hand4 = new Hand("KTJJT", 3, true);
        var hand5 = new Hand("QQQJA", 4, true);
        Hand[] hands = [hand3, hand5, hand4, hand1, hand2];
        var expectedCards = hands.OrderBy(x => x.Bid).Select(x => x.Cards).ToArray();

        // Act
        var sorted = hands.OrderBy(x => x).ToArray();

        // Assert
        for (var i = 0; i < expectedCards.Length; i++)
        {
            sorted[i].Cards.Should().Be(expectedCards[i]);
        }
    }

    [Fact]
    public void Hands_SortedMulitipliedBidWithJoker_Example()
    {
        // Arrange
        var hands = ExampleInput.Select(x => x[0].ToString())
            .Select(x => x.ParseHand(true))
            .ToArray();

        // Act
        var sum = hands.OrderBy(x => x)
                       .Select((x, i) => x.Bid * (i + 1))
                       .Sum();

        // Assert
        Debug.WriteLine($"Total bids multiplied by ranks:{sum}");

        sum.Should().Be(5905);
    }

    [Fact]
    public void Hands_SortedMulitipliedBid_Part2()
    {
        // Arrange
        var hands = InputReader.ReadLinesForDay(7)
            .Select(x => x.ParseHand(true))
            .ToArray();

        // Act
        var sum = hands.OrderBy(x => x)
                       .Select((x, i) => x.Bid * (i + 1))
                       .Sum();

        // Assert
        Debug.WriteLine($"Total bids multiplied by ranks:{sum}");

        sum.Should().BeGreaterThan(0);
        sum.Should().BeLessThan(100000);
    }
}
