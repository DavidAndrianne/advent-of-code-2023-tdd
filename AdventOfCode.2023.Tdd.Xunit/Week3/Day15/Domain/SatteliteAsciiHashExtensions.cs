namespace AdventOfCode2023.Tdd.Xunit.Week3.Day15.Domain;

public static class SatteliteAsciiHashExtensions
{
    public static int ToInitializationAsciiHash(this string input)
    {
        var value = 0;
        foreach (var character in input)
        {
            value = value.ToInitializationAsciiHash(character);
        }
        return value;
    }

    public static int ToInitializationAsciiHash(this int seed, char c) 
        => (seed + c.ToAsciiInt()) * 17 % 256;

    // https://stackoverflow.com/a/43370886
    private static int ToAsciiInt(this char c)
    {
        if (c > 127)
        {
            throw new Exception(string.Format(@"{0} (code \u{1:X04}) is not ASCII!", c, (int)c));
        }
        return c;
    }
}
