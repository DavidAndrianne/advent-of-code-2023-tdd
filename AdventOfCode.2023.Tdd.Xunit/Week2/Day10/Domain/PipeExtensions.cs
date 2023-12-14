using System.Text;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day10.Domain;

public static class PipeExtensions
{
    public static string Stringify(this MetalIslandPipe[] subloop, int matchedPipeX = -1, int matchedPipeY = -1)
    {
        var maxX = subloop.Select(x => x.X).Max();
        var maxY = subloop.Select(x => x.Y).Max();
        var sb = new StringBuilder();
        sb.AppendLine();
        for (var y = subloop.Select(x => x.Y).Min(); y <= maxY; y++)
        {
            for (var x = subloop.Select(x => x.X).Min(); x <= maxX; x++)
            {
                var character = matchedPipeX == x && matchedPipeY == y 
                    ? '1'
                    : subloop.FirstOrDefault(pipe => pipe.X == x && pipe.Y == y)?.Symbol?.Symbol 
                        ?? '.';
                sb.Append($"{character}");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string RemoveOuterCharacters(this string input)
    {
        var lines = input.Trim()
            .Split(Environment.NewLine)
            .Skip(1)
            .ToArray();
        return lines.Take(lines.Length-1)
            .Select(x => x.Substring(1, x.Length - 2))
            .Aggregate((aggr, next) => $"{aggr}{Environment.NewLine}{next}");
    }
}