using AdventOfCode2023.Tdd.Xunit.Week1.Day4.Domain;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day4.Extensions;

public static class ScratchCardExtensions
{
    public static Regex CardRegex = new Regex(@"Card\s+(\d+):([\d\s]+)\|([\d\s]+)");

    public static ScratchCard ParseScratchCard(this string line)
    {
        var match = CardRegex.Match(line);
        if (!match.Success) throw new ArgumentException($"'{line}' couldn't be parsed into valid format of 'Card id: x y | x z'");

        var cardId = match.Groups[1].Value;
        var winningNumbers = match.Groups[2].Value;
        var entries = match.Groups[3].Value;
        return new ScratchCard(int.Parse(cardId), winningNumbers.ParseIntsFromTxt(), entries.ParseIntsFromTxt());
    }

    public static int[] ParseIntsFromTxt(this string input) 
        => input.Split(" ")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse)
            .ToArray();
}
