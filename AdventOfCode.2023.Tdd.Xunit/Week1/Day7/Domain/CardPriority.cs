namespace AdventOfCode2023.Tdd.Xunit.Week1.Day7.Domain;

public static class CardPriority
{
    public static string Cards = "AKQJT98765432";
    public static string CardsJoker = "AKQT98765432J";

    public static int Get(char card) => Cards.IndexOf(card);
    public static int GetJoker(char card) => CardsJoker.IndexOf(card);
}