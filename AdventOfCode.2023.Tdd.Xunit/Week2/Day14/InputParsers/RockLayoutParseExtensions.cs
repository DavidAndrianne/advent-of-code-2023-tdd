using AdventOfCode2023.Tdd.Xunit.Week2.Day13.InputParsers;
using AdventOfCode2023.Tdd.Xunit.Week2.Day14.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day14.InputParsers;

public static class RockLayoutParseExtensions
{
    public static List<RockLayout> ParseRockLayouts(this char[][] grid)
    {
        var result = new List<RockLayout>();
        var nextLayout = new List<char[]>();
        foreach (var observation in grid)
        {
            if (!observation.IsEmpty())
            {
                nextLayout.Add(observation);
                continue;
            }
            if(nextLayout.Any()) result.Add(new RockLayout(nextLayout.ToArray()));
            nextLayout = new List<char[]>();
        }

        if (nextLayout.Any()) result.Add(new RockLayout(nextLayout.ToArray()));

        return result;
    } 
}
