using System.Linq;
using System.Text;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day13.Domain;

public class LavaIslandObservation
{
    public char[][] Observation { get; set; }
    public LavaIslandObservation(char[][] input)
    {
        Observation = input;
    }

    public string[] GetHorizontalLines() => Observation.Select(x => new string(x)).ToArray();
    public string[] GetVerticalLines()
    {
        var result = new string[Observation.First().Length];
        for (var i = 0; i < Observation.First().Length; i++)
        {
            var sb = new StringBuilder();
            for (var j = 0; j < Observation.Length; j++) sb.Append(Observation[j][i]);
            result[i] = sb.ToString();
        }
        return result;
    }

    public int HorizontalMirrorsCount()
    {
        var i = DeduceHorizontalMirrorLocation();
        return i.HasValue ? i.Value + 1 : 0;
    }

    public int VerticalMirrorsCount()
    {
        var i = DeduceVerticalMirrorLocation();
        return i.HasValue ? i.Value + 1 : 0;
    }

    public int? DeduceHorizontalMirrorLocation(bool isSmudged = false)
    {
        var lines = GetHorizontalLines();
        var original = GetMirroredLinesLocation(lines);
        return isSmudged 
            ? GetSmudgedMirroredLocation(lines, original)
            : original;
    }

    public int? DeduceVerticalMirrorLocation(bool isSmudged = false)
    {
        var lines = GetVerticalLines();
        var original = GetMirroredLinesLocation(lines);
        return isSmudged 
            ? GetSmudgedMirroredLocation(lines, original)
            : original;
    }

    protected int? GetSmudgedMirroredLocation(string[] lines, int? original)
    {
        for(var x = 0; x < lines.Length; x++)
        {
            for(var smudgeIndex = 0; smudgeIndex < lines[x].Length; smudgeIndex++)
            {
                var cleanLines = lines.Take(x).ToList();
                cleanLines.Add(InvertCharAtIndex(lines[x], smudgeIndex));
                cleanLines.AddRange(lines.Skip(x + 1));
                var result = GetMirroredLinesLocation(cleanLines.ToArray(), original);
                if (result != null && result != original) return result;
            }
        }
        return null;
    }

    public static string InvertCharAtIndex(string input, int index)
    {
        var line = input.Take(index).ToList();
        line.Add(input[index] == '.' ? '#' : '.');
        return new string(line.Concat(input.Skip(index + 1)).ToArray());
    }

    protected int? GetMirroredLinesLocation(string[] lines, int? ignoreResult = null)
    {
        for (var i = 0; i < lines.Length-1; i++)
        {
            var isMirror = true;
            var mirrorLineIndex = i;
            for (var j = i+1; mirrorLineIndex > -1 && j < lines.Length; j++)
            {
                var mirroredLine = lines[mirrorLineIndex];
                var line = lines[j];
                if (line == mirroredLine)
                {
                    mirrorLineIndex--;
                }
                else
                {
                    isMirror = false;
                    break;
                }
            }
            if (isMirror && (ignoreResult == null || i != ignoreResult)) return i;
        }

        return null;
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Observation.Select(x => new string(x)));
    }
}
