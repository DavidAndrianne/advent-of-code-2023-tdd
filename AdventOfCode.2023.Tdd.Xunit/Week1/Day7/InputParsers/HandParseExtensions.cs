using AdventOfCode2023.Tdd.Xunit.Week1.Day7.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day7.InputParsers;

public static class HandParseExtensions
{
    public static Hand ParseHand(this string line, bool isApplyingJoker = false)
    {
        var parts = line.Split(' ');
        return new Hand(parts[0], double.Parse(parts[1].Trim()), isApplyingJoker);
    }
}
