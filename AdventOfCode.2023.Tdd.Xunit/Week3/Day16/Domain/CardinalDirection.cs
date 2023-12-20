namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;

public class CardinalDirection
{
    public static CardinalDirection North = new CardinalDirection(nameof(North));
    public static CardinalDirection East = new CardinalDirection(nameof(East));
    public static CardinalDirection South = new CardinalDirection(nameof(South));
    public static CardinalDirection West = new CardinalDirection(nameof(West));
    public string Name { get; set; }
    public CardinalDirection(string name) => Name = name;
}