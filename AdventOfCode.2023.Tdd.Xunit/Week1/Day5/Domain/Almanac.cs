using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class Almanac
{
    public IEnumerable<Seed> Seeds { get; set; }
    public IEnumerable<AlmanacMapSource> MapSources { get; set; }

    public Almanac(IEnumerable<Seed> seeds, IEnumerable mapSources)
    {
        Seeds = seeds;
        MapSources = mapSources;
    }
}