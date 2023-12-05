namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class AlmanacEntry
{
    public AlmanacMapSource Source { get; set; }
    public AlmanacMapSource Destination { get; set; }
    public int SourceRangeStartIndex { get; set; }
    public int DestinationRangeStartIndex { get; set; }
    public int RangeLength { get; set; }
}
