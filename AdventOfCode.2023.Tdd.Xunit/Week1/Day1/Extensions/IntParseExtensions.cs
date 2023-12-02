using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day1.Extensions;

public static class IntParseExtensions
{
    public static Regex NumbersPattern = new Regex(@"\d+");
    public static int ParseToFirstAndLastInt(this string input, bool isReadingDigitsAsTexts = false)
    {
        if (isReadingDigitsAsTexts) input = input.ReadFirstAndLastDigitsAsInt();

        var numbers = NumbersPattern.Matches(input);
        if (!numbers.Any()) return 0;

        var numberString = numbers.Select(x => x.Value)
            .Aggregate((x, y) => $"{x}{y}");

        var numberStringToParse = new string(new[] { numberString.First(), numberString.Last() });

        return int.TryParse(numberStringToParse, out var parsedInt) ? parsedInt : 0;
    }

    private static string ReadFirstAndLastDigitsAsInt(this string input)
    {
        var firstMatchedDigit = getFirstDigitOccurance(input);
        if (firstMatchedDigit == null) return input;
        var result = input.ReplaceFirstOccurance(firstMatchedDigit, DigitTexts[firstMatchedDigit]);

        var lastMatchedDigit = getLastDigitOccurance(result);
        if (lastMatchedDigit == null) return result;
        result = result.ReplaceLastOccurance(lastMatchedDigit, DigitTexts[lastMatchedDigit]);

        return result;
    }

    public static Dictionary<string, string> DigitTexts = new Dictionary<string, string>
    {
        // retaining the first/last letter for "oneight", "twone", "threeight", ... cases (kudos reddit)
        { "one", "o1e" },
        { "two", "t2o" },
        { "three", "t3e" },
        { "four", "f4r" },
        { "five", "f5e" },
        { "six", "s6x" },
        { "seven", "s7n" },
        { "eight", "e8t" },
        { "nine", "n9e" },
    };
    public static string? getLastDigitOccurance(string input)
        => input.LookupIndexForStrings(DigitTexts.Keys)
                .Where(x => x.Value != -1)
                .OrderByDescending(x => x.Value)
                .FirstOrDefault()
                .Key;

    public static string? getFirstDigitOccurance(string input)
        => input.LookupIndexForStrings(DigitTexts.Keys)
                .Where(x => x.Value != -1)
                .OrderBy(x => x.Value)
                .FirstOrDefault()
                .Key;

    public static Dictionary<string, int> LookupIndexForStrings(this string input, IEnumerable<string> keys)
        => keys.ToDictionary(
            key => key,
            key => new Regex(key).Matches(input)
            .FirstOrDefault()
            ?.Index 
            ?? -1);

    public static string ReplaceFirstOccurance(this string source, string find, string replacement)
        => new Regex(find).Replace(source, replacement, 1);

    // https://stackoverflow.com/a/14826068
    public static string ReplaceLastOccurance(this string source, string find, string replacement)
    {
        var index = source.LastIndexOf(find);
        if(index == -1) return source;
        return source.Remove(index, find.Length)
                     .Insert(index, replacement);
    }
}
