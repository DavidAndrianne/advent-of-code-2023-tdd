using System.Text;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day11.Domain;

public static class UniverseReadingExtensions
{
    public static decimal PairWith(this int a, int b)
    {
        var small = a < b ? a : b;
        var big = a > b ? a : b;
        return decimal.Parse($"{small}.{big:D3}");
    }

    public static string StringifyNestedArray<T>(this T[][] nestedArray)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < nestedArray.Length; i++)
        {
            for (var j = 0; j < nestedArray.First().Length; j++)
                sb.Append(nestedArray[i][j]);
            sb.AppendLine();
        }
        return sb.ToString();
    }
}