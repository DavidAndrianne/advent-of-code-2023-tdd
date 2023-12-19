namespace AdventOfCode2023.Tdd.Xunit.Util;

public static class InputReader
{
    public static string[] ReadLinesForDay(int dayNumber) 
        => File.ReadAllLines(GetPathForDay(dayNumber));

    public static string ReadRawTextForDay(int dayNumber) 
        => File.ReadAllText(GetPathForDay(dayNumber));

    public static char[][] ReadCharGrid(int dayNumber) 
        => File.ReadAllText(GetPathForDay(dayNumber))
            .Split($"{Environment.NewLine}")
            .Select(x => x.ToArray())
            .ToArray();

    private static string GetPathForDay(int dayNumber)
    {
        var weekNumber = (int)Math.Ceiling(dayNumber / 7M);
        return $"Week{weekNumber}/Day{dayNumber}/Input.txt";
    }
}
