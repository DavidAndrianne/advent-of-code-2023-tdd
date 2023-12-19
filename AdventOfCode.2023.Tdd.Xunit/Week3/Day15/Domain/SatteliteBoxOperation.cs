namespace AdventOfCode2023.Tdd.Xunit.Week3.Day15.Domain;

public class SatteliteBoxOperation
{
    public const char PutIntoBoxSign = '=';
    public const char RemoveFromBoxSign = '-';
    public static readonly SatteliteBoxOperation Put = new(PutIntoBoxSign);
    public static readonly SatteliteBoxOperation Remove = new(RemoveFromBoxSign);

    public char Sign { get; protected set; }

    public SatteliteBoxOperation(char sign) => Sign = sign;
}