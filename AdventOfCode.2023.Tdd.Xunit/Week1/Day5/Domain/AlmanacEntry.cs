
namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class AlmanacEntry
{
    public AlmanacMapSource Source { get; set; }
    public AlmanacMapSource Target { get; set; }
    public List<AlmanacEntryRange> Ranges { get; set; }

    public AlmanacEntry(AlmanacMapSource source, AlmanacMapSource destination, List<AlmanacEntryRange> ranges)
    {
        Source = source;
        Target = destination;
        Ranges = ranges.OrderBy(x => x.TargetRangeStartIndex).ToList(); // favor lower values
    }

    public override string ToString()
        => $"{Source} => {Target}: {Ranges.Select(x => $"{x}\n").ToArray()}";

    public double GetTargetIndexForSourceIndex(double index)
    {
        var applicableRanges = Ranges.Where(x => x.IsWithinRange(index)).ToArray();
        if (!applicableRanges.Any()) return index;

        var applicableIndexes = applicableRanges.Select(x =>
        {
            var delta = index - x.SourceRangeStartIndex;
            return x.TargetRangeStartIndex + delta;
        }).ToArray();
        return applicableIndexes.Min(); // favor lower values
    }
}
