using AdventOfCode2023.Tdd.Xunit.Week1.Day6.Domain;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day6.InputParsers;

public static class RaceParseExtensions
{
    public static Race[] ParseRaces(this string[] lines)
    {
        var times = new List<double>();
        var distances = new List<double>();
        foreach(string line in lines)
        {
            if (line.StartsWith("Time:")) times.AddRange(line.ParseIntsFromLine());
            else if (line.StartsWith("Distance:")) distances.AddRange(line.ParseIntsFromLine());
            else throw new ArgumentException($"Line not recognized as time or distance '{line}'");
        }

        if (times.Count != distances.Count) throw new ArgumentException($"Identified {times.Count} times for {distances.Count} distances");

        return times.Select((x,i) => new Race(x, distances[i])).ToArray();
    }

    public static Regex IntPattern = new Regex(@"\d+");
    public static double[] ParseIntsFromLine(this string line)
        => IntPattern.Matches(line).Select(x => double.Parse(x.Value)).ToArray();

    public static Race ParseSingleRace(this string[] lines)
    {
        double time = 0;
        double distance = 0;

        foreach (string line in lines)
        {
            if (line.StartsWith("Time:")) time = line.Replace(" ", "").ParseIntsFromLine().First();
            else if (line.StartsWith("Distance:")) distance = line.Replace(" ", "").ParseIntsFromLine().First();
            else throw new ArgumentException($"Line not recognized as time or distance '{line}'");
        }

        return new Race(time, distance);
    }
}
