using AdventOfCode2023.Tdd.Xunit.Week2.Day12.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day12.InputParsers;

public static class SpringRecordParseExtensions
{
    public static SpringRecord ToSpringRecord(this string input)
    {
        var parts = input.Split(' ');
        var ledger = parts[0];
        var damagedGroups = parts[1].Split(',')
            .Select(i => int.Parse(i.Trim()))
            .ToArray();
        return new SpringRecord(ledger, damagedGroups);
    }

    public static string UnfoldRecordLine(this string input)
    {
        var parts = input.Split(' ');
        var ledger = parts[0];
        var damagedGroups = parts[1];
        return $"{ledger}?{ledger}?{ledger}?{ledger}?{input},{damagedGroups},{damagedGroups},{damagedGroups},{damagedGroups}";
    }
}
