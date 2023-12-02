using AdventOfCode2023.Tdd.Xunit.Week1.Day2.Domain
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day2.Extensions;

public static class GameExtensions
{
    public static Regex GameRegex = new Regex(@"Game (\d+):");
    public static Regex RedRegex = new Regex(@"(\d+)\sred,?");
    public static Regex GreenRegex = new Regex(@"(\d+)\sgreen,?");
    public static Regex BlueRegex = new Regex(@"(\d+)\sblue,?");

    public static Game ParseGame(this string line)
    {
        var gameId = GameRegex.Match(line).Groups.Values.Skip(1).First().Value;
        var redCounts = RedRegex.ParseIntsFromFirstCaptureGroup(line);
        var greenCounts = GreenRegex.ParseIntsFromFirstCaptureGroup(line);
        var blueCounts = BlueRegex.ParseIntsFromFirstCaptureGroup(line);
        return new Game(int.Parse(gameId), redCounts, greenCounts, blueCounts);
    }

    public static int[] ParseIntsFromFirstCaptureGroup(this Regex regex, string input) 
        => regex.Matches(input)
            .SelectMany(x => 
                x.Groups
                .Values
                .Skip(1)
                .Select(g => int.Parse(g.Value)))
            .ToArray();
}
