using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Domain;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day3.Extensions;

public static class PartNumberExtensions
{
    public static Regex SymbolRegex = new Regex(@"(?![\d\.]).");
    public static Regex NumberRegex = new Regex(@"\d+");

    public static GondolaManual ParseGondolaManual(this string input)
    {
        var lines = input.Split("\r\n");
        var numbers = lines.SelectMany((l, i) => l.RegexParseToDto(i, NumberRegex, ToPartNumber));
        var symbols = lines.SelectMany((l, i) => l.RegexParseToDto(i, SymbolRegex, ToPartNumberSymbol));
        return new GondolaManual(numbers, symbols);
    }

    public static T[] RegexParseToDto<T>(this string line, int index, Regex regex, Func<Group, int, T> mapper) 
        => regex.Matches(line)
            .SelectMany(m => m.Groups.Values)
            .Select(g => mapper(g, index))
            .ToArray();

    public static PartNumber ToPartNumber(this Group group, int line)
    {
        var capturedNumber = group.Captures.First();
        return new PartNumber(line, capturedNumber.Index, capturedNumber.Value);
    }

    public static PartNumberSymbol ToPartNumberSymbol(this Group group, int line)
    {
        var capturedSymbol = group.Captures.First();
        return new PartNumberSymbol(line, capturedSymbol.Index, capturedSymbol.Value);
    }
}