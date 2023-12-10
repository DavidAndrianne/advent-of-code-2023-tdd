using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;
using AdventOfCode2023.Tdd.Xunit.Week1.Day5.InputParsers;
using FluentAssertions;
using System.Diagnostics;
using Range = AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain.Range;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5;

public class Day5Tests
{
    public readonly string ExampleInput = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    [Fact]
    public void ParseAlmanac_WithValidInput_ReturnsCorrectValues()
    {
        // Arrange & Act
        var almanac = ExampleInput.ParseAlmanac();

        // Assert
        almanac.Seeds.Select(x => x.Id).ToArray().Should().BeEquivalentTo([79, 14, 55, 13]);

        almanac.Entries.Count().Should().Be(AlmanacMapSource.All.Count()-1, "all sources are present");

        almanac.Entries[AlmanacMapSource.Seed.Name].Ranges.Count().Should().Be(2, "there are 2 ranges");
        almanac.Entries[AlmanacMapSource.Seed.Name].Ranges.First().ToString().Should().Be(new AlmanacEntryRange(98, 50, 2).ToString());
        almanac.Entries[AlmanacMapSource.Seed.Name].Ranges.Last().ToString().Should().Be(new AlmanacEntryRange(50, 52, 48).ToString());
    }

    public static object[][] ExampleTargetIndexDataset = [
        [0, 0, "not within range"],
        [49, 49, "not within range"],
        [50, 52, "source to target index"],
        [51, 53, "source to target within range2"],
        [79, 81, "source to target within range2"],
        [96, 98, "source to target index range1"],
        [97, 99, "source to target index range1"]
    ];
    [Theory]
    [MemberData(nameof(ExampleTargetIndexDataset))]
    public void AlmanacEntry_GetTargetIndexForSourceIndex_ReturnsCorrectIndex(int sourceIndex, int expectedTargetIndex, string because)
    {
        // Arrange
        var tinyAlmanac = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48";
        var almanac = tinyAlmanac.ParseAlmanac();
        var sut = almanac.Entries[AlmanacMapSource.Seed.Name];

        // Act
        var targetIndex = sut.GetTargetIndexForSourceIndex(sourceIndex);

        // Assert
        targetIndex.Should().Be(expectedTargetIndex, because);
    }

    public static object[][] ExampleLocationTargetIndexDataset = [
        [79, 82, "this is the nearest location"],
        [14, 43, "this is the nearest location"],
        [55, 86, "this is the nearest location"],
        [13, 35, "this is the nearest location"]
    ];
    [Theory]
    [MemberData(nameof(ExampleLocationTargetIndexDataset))]
    public void Almanac_ConsultWhereToPlant_ReturnsNearestLocationIndex(int seedI, int locationI, string because)
    {
        //Arrange
        var seed = new Seed(seedI);
        var sut = ExampleInput.ParseAlmanac();

        // Act
        var location = sut.ConsultWhereToPlant(seed);

        // Assert
        location.Id.Should().Be(locationI, because);
    }

    [Fact]
    public void Almanac_ConsultWhereToPlant_Part1()
    {
        // Arrange
        var almanac = InputReader.ReadRawTextForDay(5).ParseAlmanac();

        // Act
        var minLocation = almanac.Seeds
            .Select(x => almanac.ConsultWhereToPlant(x).Id)
            .Min();

        // Assert
        Debug.WriteLine($"MinLocation :{minLocation}");

        minLocation.Should().BeGreaterThan(0);
        minLocation.Should().BeLessThan(100000);
    }


    [Fact]
    public void RangeEquals_WithEqualRange_IsEqual()
    {
        // Arrange
        var r1 = new Range(0, 1);
        var r2 = new Range(0, 1);

        // Act
        var equals2 = r1.Equals(r2);

        // Assert
        equals2.Should().BeTrue("because their start & end are identical");
    }
    
    [Fact]
    public void RangeEquals_WithNull_IsNotEqual()
    {
        // Arrange
        var r1 = new Range(0, 1);

        // Act
        var equals2 = r1.Equals(null);

        // Assert
        equals2.Should().BeFalse("because range is null");
    }

    public static object[][] RangeOverlapData = [
        [new Range(0, 10), new Range(0, 10), new Range(0, 10), "the overlap is identical as the ranges are equal"],
        [new Range(0, 10), new Range(0, 15), new Range(0, 10), "range2 overlaps range1"],
        [new Range(10, 15), new Range(9, 16), new Range(10, 15), "range2 overlaps range1"],
        [new Range(10, 20), new Range(15, 20), new Range(15, 20), "range1 overlaps range2"],
        [new Range(10, 20), new Range(21, 30), null, "no overlap"],
        [new Range(101, 120), new Range(120, 130), new Range(120, 120), "teeny overlap end"],
        [new Range(101, 120), new Range(101, 101), new Range(101, 101), "teeny overlap start"],
    ];
    [Theory]
    [MemberData(nameof(RangeOverlapData))]
    public void Range_GetOverlap_ReturnsOverlap(Range range1, Range range2, Range? expected, string because)
    {
        // Arrange & Act
        var isOverlapping = range1.IsOverlapping(range2);
        var overlap = range1.GetOverlappingRange(range2);

        // Assert
        isOverlapping.Should().Be(expected != null, "range1 & range2 overlap");
        if (expected == null) return;
        
        overlap.Start.Should().Be(expected.Start, because);
        overlap.End.Should().Be(expected.End, because);
    }

    public static object[][] RangeSubtractData = [
        [new Range(0, 10), new Range(0, 10), null, null, "the ranges are equal"],
        [new Range(0, 10), new Range(0, 15), null, new Range(11, 15), "range2 overlaps range1"],
        [new Range(10, 15), new Range(9, 16), new Range(9, 9), new Range(16, 16), "range2 overlaps range1"],
        [new Range(10, 20), new Range(15, 20), new Range(10, 14), null, "range1 overlaps range2"],
        [new Range(10, 20), new Range(21, 30), new Range(10, 20), new Range(21, 30), "no overlap"],
        [new Range(101, 120), new Range(120, 130), new Range(101, 119), new Range(121, 130), "teeny overlap end"],
        [new Range(101, 120), new Range(101, 101), null, new Range(102, 120), "teeny overlap start"],
    ];
    [Theory]
    [MemberData(nameof(RangeSubtractData))]
    public void Range_Subtract_ReturnsNonOverlap(Range range1, Range range2, Range? expected1, Range? expected2, string because)
    {
        // Arrange & Act
        var overlap = range1.Subtract(range2);

        // Assert
        if(expected1 != null)
        {
            overlap.part1.Should().NotBeNull(because);
            overlap.part1.Start.Should().Be(expected1.Start, because);
            overlap.part1.End.Should().Be(expected1.End, because);
        }

        if (expected2 != null) {
            overlap.part2.Should().NotBeNull(because);
            overlap.part2.Start.Should().Be(expected2.Start, because);
            overlap.part2.End.Should().Be(expected2.End, because);
        }
    }

    [Fact]
    public void Almanac_ConsultWhereToPlant_Part2()
    {
        // Arrange
        var almanac = InputReader.ReadRawTextForDay(5).ParseAlmanac();

        // Act
        var lowestLocation = almanac.ConsultWhereToPlantRanges();

        // Assert
        lowestLocation.Id.Should().BeGreaterThan(0);
        lowestLocation.Id.Should().BeLessThan(1549152);
    }
}
