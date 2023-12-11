namespace AdventOfCode2023.Tdd.Xunit.Util;

public static class InputReader
{
    public static string[] ReadLinesForDay(int dayNumber)
    {
        var weekNumber = dayNumber == 7 ? 1 : dayNumber / 7 + 1;
        return File.ReadAllLines($"Week{weekNumber}/Day{dayNumber}/Input.txt");
    }

    public static string ReadRawTextForDay(int dayNumber)
    {
        var weekNumber = dayNumber / 7 + 1;
        return File.ReadAllText($"Week{weekNumber}/Day{dayNumber}/Input.txt");
    }
}
