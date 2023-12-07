using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.InputParsers;

public static class AlmanacParseExtensions
{
    public static Regex SeedRegex = new Regex(@"seeds: (.*\d+.*)\n");
    public static Regex MapSourceRegex = new Regex(@"(\w+)-to-(\w+) map:\r\n([\s\d\r\n]+)");
    public static Almanac ParseAlmanac(this string input)
    {
        var seedMatch = SeedRegex.Match(input);
        if (!seedMatch.Success) throw new ArgumentException($"Seeds couldn't be parsed from {nameof(input)}");
        var seeds = seedMatch.Groups[1].Value.Trim().Split(" ")
            .Select(i => double.Parse(i))
            .Select(id => new Seed(id))
            .ToArray();

        var mapSourceMatches = MapSourceRegex.Matches(input);
        if (!mapSourceMatches.Any()) throw new ArgumentException($"No mapsources could be parsed from {nameof(input)}");

        var mapSources = mapSourceMatches.Select(ParseToAlmanacEntry).ToArray();

        return new Almanac(seeds, mapSources);
    }

    private static AlmanacEntry ParseToAlmanacEntry(Match m)
    {
        var source = m.Groups[1].Value;
        var destination = m.Groups[2].Value;
        var almanacSource = AlmanacMapSource.Parse(source);
        var almanacDestination = AlmanacMapSource.Parse(destination);

        var ranges = m.Groups[3].Value
            .Trim() // remove \r\n
            .Split("\r\n")
            .Select(x =>
            {
                var rangeInput = x.Split(" ")
                    .Select(x => double.Parse(x))
                    .ToArray();
                if (rangeInput.Length != 3) throw new ArgumentException($"{x} didn't contain a valid 3 ints");
                return new AlmanacEntryRange(rangeInput[1], rangeInput[0], rangeInput[2]);
            })
            .ToList();

        return new AlmanacEntry(almanacSource, almanacDestination, ranges);
    }
}
