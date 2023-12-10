
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

    public Range[] GetTargetRangesForSourceRange(Range sourceRange)
    {
        var applicableRanges = Ranges.Select(x 
            => new {
                Delta = x.TargetRangeStartIndex - x.SourceRangeStartIndex,
                Range = new Range(x.SourceRangeStartIndex, x.SourceRangeStartIndex+x.RangeLength)
            })  
            .Select(x => new { Delta = x.Delta, OverlappingRange = x.Range.GetOverlappingRange(sourceRange) })
            .Where(x => x.OverlappingRange != null)
            .ToArray();

        // Run through each overlap of a matched range
        var result = new List<Range>();
        Range[] sourceRanges = [sourceRange];
        foreach (var applicableRange in applicableRanges)
        {
            // translate the source into target range
            var overlappingRange = applicableRange.OverlappingRange;
            var delta = applicableRange.Delta;

            sourceRanges = sourceRanges.SelectMany(x => {
                var subtractResult = x.Subtract(overlappingRange!);
                var translatedRange = new Range(overlappingRange!.Start + delta, overlappingRange.End + delta);
                result.Add(translatedRange);

                // retain the source indexes that didn't match this range
                Range[] unmappedSourceRanges = subtractResult.part1 == null || subtractResult.part2 == null
                    ? [] // fully mapped, no source left
                    : [subtractResult.part1, subtractResult.part2];
                return unmappedSourceRanges;
            })
            .ToArray();

            if (!sourceRanges.Any()) break; // no more source left to map
        }
        result.AddRange(sourceRanges); // 1:1 mapping

        return result.Where(x => x != null)
            .ToArray();
    }
}