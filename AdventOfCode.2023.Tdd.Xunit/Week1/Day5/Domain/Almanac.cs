namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class Almanac
{
    public IEnumerable<Seed> Seeds { get; set; }
    public IEnumerable<Range> GetSeedsAsRanges()
    {
        var seedRanges = new List<Range>();
        var seedsArr = Seeds.ToArray();
        for (var s = 0; s < Seeds.Count(); s += 2)
        {
            seedRanges.Add(new Range(seedsArr[s].Id, seedsArr[s + 1].Id));
        }
        return seedRanges;
    }
    public Dictionary<string, AlmanacEntry> Entries { get; set; }

    public Almanac(IEnumerable<Seed> seeds, IEnumerable<AlmanacEntry> entries)
    {
        Seeds = seeds;
        Entries = entries.ToDictionary(x => x.Source.Name, x => x);
    }

    public Location ConsultWhereToPlant(Seed seed)
    {
        var reference = FindReference(AlmanacMapSource.Seed, AlmanacMapSource.Location, seed.Id);
        return new Location() { Id = reference.TargetIndex };
    }

    public AlmanacMapReference FindReference(
        AlmanacMapSource source, 
        AlmanacMapSource target,
        double index)
    {
        var entry = Entries[source.Name];
        var targetIndex = entry.GetTargetIndexForSourceIndex(index);
        var reference = new AlmanacMapReference(entry, index, targetIndex);

        if (reference.Entry.Target.Name == target.Name) return new AlmanacMapReference(entry, index, targetIndex);

        return FindReference(reference.Entry.Target, target, reference.TargetIndex);
    }



    public Location ConsultWhereToPlantRanges()
    {
        var seedRanges = GetSeedsAsRanges().ToArray();
        var nextMap = Entries[AlmanacMapSource.Seed.Name];
        var lowestLocation = LowestLocation(seedRanges, nextMap);
        return new Location() { Id = lowestLocation };
    }

    public double LowestLocation(Range[] rangesToTranslate, AlmanacEntry nextMap)
    {
        var translatedRanges = rangesToTranslate.SelectMany(nextMap.GetTargetRangesForSourceRange)
            .Where(x => x != null)
            .ToArray();

        if (nextMap.Target.Name == AlmanacMapSource.Location.Name) {
            return translatedRanges.OrderBy(x => x.Start)
                .Select(x => x.Start)
                .Min();
        }

        return LowestLocation(translatedRanges, Entries[nextMap.Target.Name]);
    }
}
