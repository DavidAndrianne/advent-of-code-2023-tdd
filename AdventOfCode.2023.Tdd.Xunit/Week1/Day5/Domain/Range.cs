namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class Range : IEquatable<Range>
{
    public double Start { get; set; }
    public double End { get; set; }
    public Range(double start, double end)
    {
        Start = start < end ? start : end;
        End = end > start ? end : start;
    }

    public bool IsOverlapping(Range? other) 
        => GetOverlappingRange(other) != null;

    public Range? GetOverlappingRange(Range? other)
    {
        if (other == null) return null;

        var range1 = Start <= other.Start 
            ? this 
            : other;
        var range2 = Start <= other.Start
            ? other
            : this;

        if (range1.Start == range2.Start)
        {
            return range1.End == range2.End
                ? new Range(range1.Start, range1.End) // equal
                : range2.End < range1.End
                    ? new Range(range1.Start, range2.End) // start equal, range2 wrapped in range1
                    : new Range(range1.Start, range1.End); // start equal, range1 wrapped in range2
        }

        // range1 start < range2 start < range2 end < range1 end
        if (range1.Start <= range2.Start && range2.End <= range1.End)
        {
            return new Range(range2.Start, range2.End); // second range is fully enwrapped in range1
        }

        // range1 start < range2 start < range1 end
        if (range1.Start <= range2.Start && range2.Start == range1.End)
        {
            return new Range(range2.Start, range2.Start); // second range is partially enwrapped in range1
        }

        return null; // no overlap
    }

    public (Range? part1, Range? part2) Subtract (Range other)
    {
        var range1 = Start <= other.Start
            ? this
            : other;
        var range2 = Start <= other.Start
            ? other
            : this;

        if (range1?.Start == range2.Start && range1?.End == range2.End) return (null, null); // identical, remove everything

        var overlapToSubtract = GetOverlappingRange(other);
        if (overlapToSubtract == null) return (range1, range2); // No overlap, return original ranges
        
        var part1 = range1.Start == range2.Start
            ? null 
            : new Range(range1.Start, overlapToSubtract.Start-1);
        var part2 = range1.End == range2.End
            ? null
            : new Range(overlapToSubtract.End+1, range1.End > range2.End ? range1.End : range2.End);

        return (part1, part2);
    }

    public bool Equals(Range? other) 
        => other?.Start == Start && other?.End == End;

    public override string ToString() 
        => $"{Start}-{End}";
}