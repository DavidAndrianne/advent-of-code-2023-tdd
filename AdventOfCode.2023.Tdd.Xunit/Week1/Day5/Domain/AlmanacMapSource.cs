namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class AlmanacMapSource
{
    public static AlmanacMapSource Seed => new AlmanacMapSource(nameof(Seed));
    public static AlmanacMapSource Soil => new AlmanacMapSource(nameof(Soil));
    public static AlmanacMapSource Fertilizer => new AlmanacMapSource(nameof(Fertilizer));
    public static AlmanacMapSource Water => new AlmanacMapSource(nameof(Water));
    public static AlmanacMapSource Light => new AlmanacMapSource(nameof(Light));
    public static AlmanacMapSource Temperature => new AlmanacMapSource(nameof(Temperature));
    public static AlmanacMapSource Humidity => new AlmanacMapSource(nameof(Humidity));
    public static AlmanacMapSource Location => new AlmanacMapSource(nameof(Location));

    public static AlmanacMapSource[] All => [Seed, Soil, Fertilizer, Water, Light, Temperature, Humidity, Location];

    public static AlmanacMapSource Parse(string mapSourceName) 
        => All.Single(x => x.Name.Equals(mapSourceName, StringComparison.OrdinalIgnoreCase));

    public string Name { get; set; }
    protected AlmanacMapSource(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}