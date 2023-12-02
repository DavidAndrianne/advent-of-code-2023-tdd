namespace AdventOfCode2023.Tdd.Xunit.Util;

public static class InputReader
{
    public static string[] ReadLinesForDay(int dayNumber)
    {
        var weekNumber = dayNumber / 7 + 1;
        return File.ReadAllLines($"Week{weekNumber}/Day{dayNumber}/Input.txt");
    }
}
