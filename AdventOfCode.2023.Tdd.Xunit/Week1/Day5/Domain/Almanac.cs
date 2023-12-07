namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class Almanac
{
    public IEnumerable<Seed> Seeds { get; set; }
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
        double index, 
        List<AlmanacMapReference>? priorReferences = null)
    {
        var entry = Entries[source.Name];
        var targetIndex = entry.GetTargetIndexForSourceIndex(index);
        var reference = new AlmanacMapReference(entry, index, targetIndex, priorReferences);

        if (reference.Entry.Target.Name == target.Name) return reference;

        priorReferences = priorReferences ?? new List<AlmanacMapReference>();
        priorReferences.Add(reference);
        return FindReference(reference.Entry.Target, target, reference.TargetIndex, priorReferences);
    }
}
