using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.InputParsers;

public static class AlmanacParseExtensions
{
    public static Regex SeedRegex = new Regex(@"seeds: (\d+\s)+");
    public static Regex MapSourceRegex = new Regex(@"(\w+)-to-(\w+) map:\r\n(\d+ \d+ \d+\r\n)+");
    public static Almanac ParseAlmanac(this string input)
    {
        var seedMatch = SeedRegex.Match(input);
        if (!seedMatch.Success) throw new ArgumentException($"Seeds couldn't be parsed from {nameof(input)}");


        var mapSourceMatch = MapSourceRegex.Match(input);
        if (!mapSourceMatch.Success) throw new ArgumentException($"No mapsources could be parsed from {nameof(input)}");
    }
}
