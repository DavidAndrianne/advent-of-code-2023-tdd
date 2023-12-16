using AdventOfCode2023.Tdd.Xunit.Week2.Day13.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day13.InputParsers;

public static class LavaIslandObservationParseExtensions
{
    public static List<LavaIslandObservation> ToLavaIslandObservations(this char[][] observations)
    {
        var result = new List<LavaIslandObservation>();
        var nextObservation = new List<char[]>();
        foreach(var observation in observations)
        {
            if (!observation.IsEmpty())
            {
                nextObservation.Add(observation);
                continue;
            }
            result.Add(new LavaIslandObservation(nextObservation.ToArray()));
            nextObservation = new List<char[]>();
        }

        if (nextObservation.Any()) result.Add(new LavaIslandObservation(nextObservation.ToArray()));

        return result;
    }

    private static bool IsEmpty(this char[] input) => input.Length == 0 || input.All(x => string.IsNullOrEmpty(x.ToString()));

    public static char[][] ToCharGrid(this string input)
        => input.Split(Environment.NewLine)
            .Select(x => x.ToArray())
            .ToArray();
}
