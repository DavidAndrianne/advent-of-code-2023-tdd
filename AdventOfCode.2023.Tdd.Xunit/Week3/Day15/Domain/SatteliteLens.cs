namespace AdventOfCode2023.Tdd.Xunit.Week3.Day15.Domain;

public class SatteliteLens
{
    public string Label { get; set; }
    public int? FocalLength { get; set; }

    public SatteliteLens(string label, int? focalLength = null)
    {
        Label = label;
        FocalLength = focalLength;
    }

    public override string ToString() => $"[{Label} {FocalLength}]";
}
