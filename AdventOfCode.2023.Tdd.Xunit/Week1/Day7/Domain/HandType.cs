using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day7.Domain;

public class HandType
{
    public static HandType FiveOfAKind = new HandType("Five of a kind", 1);
    public static HandType FourOfAKind = new HandType("Four of a kind", 2);
    public static HandType FullHouse = new HandType("Full house", 3);
    public static HandType ThreeOfAKind = new HandType("Three of a kind", 4);
    public static HandType TwoPair = new HandType("Two pair", 5);
    public static HandType OnePair = new HandType("One pair", 6);
    public static HandType HighCard = new HandType("High card", 7);

    public string Name { get; set; }
    public int Priority { get; set; }
    public HandType(string name, int priority)
    {
        Name = name;
        Priority = priority;
    }

    public static Regex FiveOfAKindPattern = new Regex(@"(.)\1{4}");
    public static Regex FourOfAKindPattern = new Regex(@"(.)\1{3}");

    public static Regex FullHousePattern1 = new Regex(@"(.)\1{2}(.)\2{1}");
    public static Regex FullHousePattern2 = new Regex(@"(.)\1{1}(.)\2{2}");

    public static Regex ThreeOfAKindPattern = new Regex(@"(.)\1{2}");
    public static Regex TwoPairPattern = new Regex(@"(.)\1{1}.?(.)\2{1}");
    public static Regex OnePairPattern = new Regex(@"(.)\1{1}");

    public static HandType Parse(string cards)
    {
        if (FiveOfAKindPattern.IsMatch(cards)) return FiveOfAKind;

        var orderedCards = new string(cards.OrderBy(x => CardPriority.Get(x)).ToArray());

        if (FourOfAKindPattern.IsMatch(orderedCards)) return FourOfAKind;
        if (FullHousePattern1.IsMatch(orderedCards) || FullHousePattern2.IsMatch(orderedCards)) return FullHouse;
        if (ThreeOfAKindPattern.IsMatch(orderedCards)) return ThreeOfAKind;
        if (TwoPairPattern.IsMatch(orderedCards)) return TwoPair;
        if (OnePairPattern.IsMatch(orderedCards)) return OnePair;
        return HighCard;
    }

    public static HandType ParseJoker(string cards)
    {
        var groupedCardSymbols = cards.GroupBy(x => x)
            .Select(x => new { x.Key, Count= x.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => CardPriority.GetJoker(x.Key))
            .ToList();

        var jokers = groupedCardSymbols.SingleOrDefault(x => x.Key == 'J')?.Count ?? 0;
        var mostFrequentGroupCount = groupedCardSymbols.Where(x => x.Key != 'J').FirstOrDefault()?.Count ?? 0;
        var secondMostFrequentGroupCount = groupedCardSymbols.Where(x => x.Key != 'J').Skip(1).FirstOrDefault()?.Count ?? 0;

        if (mostFrequentGroupCount + jokers == 5) return FiveOfAKind;
        if (mostFrequentGroupCount + jokers == 4) return FourOfAKind;
        if (mostFrequentGroupCount + jokers == 3 && secondMostFrequentGroupCount == 2) return FullHouse;
        if (mostFrequentGroupCount + jokers == 3) return ThreeOfAKind;
        if (mostFrequentGroupCount + jokers == 2 && secondMostFrequentGroupCount == 2) return TwoPair;
        if (mostFrequentGroupCount + jokers == 2) return OnePair;
        return HighCard;
    }

    public override string ToString() => Name;
}