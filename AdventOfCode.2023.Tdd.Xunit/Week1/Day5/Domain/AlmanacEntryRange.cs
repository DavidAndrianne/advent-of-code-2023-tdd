namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class AlmanacEntryRange {
    public double SourceRangeStartIndex { get; set; }
    public double TargetRangeStartIndex { get; set; }
    public double RangeLength { get; set; }
    public AlmanacEntryRange(double startSource, double startTarget, double length)
    {
        SourceRangeStartIndex = startSource;
        TargetRangeStartIndex = startTarget;
        RangeLength = length;
    }

    public bool IsWithinRange(double index) 
        => SourceRangeStartIndex <= index && index < (SourceRangeStartIndex + RangeLength);

    public override string ToString()
        => $"{SourceRangeStartIndex} => {TargetRangeStartIndex} .. {RangeLength}";

    public Range[] MapRanges(Range r) => [r];
}