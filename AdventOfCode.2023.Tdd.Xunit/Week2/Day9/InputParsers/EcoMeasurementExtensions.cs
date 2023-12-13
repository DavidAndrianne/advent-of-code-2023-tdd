using AdventOfCode2023.Tdd.Xunit.Week2.Day9.Domain;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day9.InputParsers;

public static class EcoMeasurementExtensions
{
    private static Regex NumberRegex = new Regex(@"-?\d+");
    public static EcoMeasurement ParseEcoMeasurement(this string line)
    {
        var values = NumberRegex.Matches(line).Select(x => double.Parse(x.Value)).ToArray();
        return new EcoMeasurement(values);
    }
}
